using UnityEngine;
using UnityEngine.InputSystem;

public class Players : Paddle
{
    [SerializeField] private bool player1;

    // Controles de la pala, editables en el Inspector. Si se deja sin bindings,
    // se usan los de por defecto: W/S y mando para el jugador 1, flechas para el 2
    [SerializeField] private InputAction moveAction = new InputAction("Move", InputActionType.Value, expectedControlType: "Axis");

    // Táctil: la pala sigue al dedo y frena al acercarse a esta distancia, para no temblar
    [SerializeField] private float touchSlowDistance = 0.5f;

    private Camera cam;
    // Con un solo jugador humano (modo IA), vale tocar en cualquier parte de la pantalla
    private bool useWholeScreen;

    protected override void Awake()
    {
        base.Awake();

        if (moveAction.bindings.Count == 0)
            AddDefaultBindings();

        // Si la pala la controla la IA, el teclado no debe moverla
        if (GetComponent<AI>() != null)
            enabled = false;
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Start()
    {
        cam = Camera.main;

        int humanPlayers = 0;
        foreach (Players p in FindObjectsByType<Players>())
        {
            if (p.enabled)
                humanPlayers++;
        }
        useWholeScreen = humanPlayers == 1;
    }

    void Update()
    {
        // El táctil tiene prioridad: si hay un dedo en la mitad de este jugador, va hacia él
        if (TryGetTouchY(out float targetY))
        {
            move = Mathf.Clamp((targetY - transform.position.y) / touchSlowDistance, -1f, 1f);
            return;
        }

        // Con teclado vale -1, 0 o 1; con el stick del mando, cualquier valor intermedio
        move = Mathf.Clamp(moveAction.ReadValue<float>(), -1f, 1f);
    }

    // Busca un dedo en la mitad de pantalla de este jugador (izquierda el 1, derecha el 2)
    // y devuelve su altura en coordenadas del mundo
    private bool TryGetTouchY(out float worldY)
    {
        worldY = 0;

        var touchscreen = Touchscreen.current;
        if (touchscreen == null || cam == null) return false;

        float half = Screen.width / 2f;
        foreach (var touch in touchscreen.touches)
        {
            if (!touch.press.isPressed) continue;

            Vector2 position = touch.position.ReadValue();
            bool inMyHalf = useWholeScreen || (player1 ? position.x < half : position.x >= half);
            if (!inMyHalf) continue;

            worldY = cam.ScreenToWorldPoint(position).y;
            return true;
        }

        return false;
    }

    private void AddDefaultBindings()
    {
        if (player1)
        {
            moveAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Keyboard>/s")
                .With("Positive", "<Keyboard>/w");
            moveAction.AddBinding("<Gamepad>/leftStick/y");
            moveAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Gamepad>/dpad/down")
                .With("Positive", "<Gamepad>/dpad/up");
        }
        else
        {
            moveAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Keyboard>/downArrow")
                .With("Positive", "<Keyboard>/upArrow");
        }
    }
}
