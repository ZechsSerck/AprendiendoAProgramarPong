using UnityEngine;
using UnityEngine.InputSystem;

public class Players : Paddle
{
    [SerializeField] private bool player1;

    // Controles de la pala, editables en el Inspector. Si se deja sin bindings,
    // se usan los de por defecto: W/S y mando para el jugador 1, flechas para el 2
    [SerializeField] private InputAction moveAction = new InputAction("Move", InputActionType.Value, expectedControlType: "Axis");

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

    void Update()
    {
        // Con teclado vale -1, 0 o 1; con el stick del mando, cualquier valor intermedio
        move = Mathf.Clamp(moveAction.ReadValue<float>(), -1f, 1f);
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
