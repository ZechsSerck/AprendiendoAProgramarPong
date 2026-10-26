using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    // Segundos hasta volver solo al menú; 0 o menos para no volver nunca solo
    [SerializeField] private float delay = 3f;
    [SerializeField] private string menuScene = "MainMenu";
    // Segundos en los que se ignora el teclado, para no saltar la pantalla sin querer
    [SerializeField] private float inputDelay = 0.5f;
    // Texto donde se muestra quién ha ganado
    [SerializeField] private TMP_Text winnerText;

    void Start()
    {
        // Si se abre la escena sin jugar (Winner = 0), se deja el texto de la escena
        if (winnerText != null && GameSession.Winner != 0)
            winnerText.text = GameSession.WinnerIsAI ? "Victoria de la IA" : $"Victoria Jugador {GameSession.Winner}";

        if (delay > 0)
            Invoke(nameof(LoadMainMenu), delay);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad < inputDelay) return;

        var kb = Keyboard.current;
        if (kb == null) return;

        // R: revancha. Enter o Esc: menú
        if (kb.rKey.wasPressedThisFrame)
            Rematch();
        else if (kb.enterKey.wasPressedThisFrame || kb.escapeKey.wasPressedThisFrame)
            LoadMainMenu();
    }

    // Vuelve a jugar en la misma escena (y con la misma dificultad) que la última partida
    public void Rematch()
    {
        if (string.IsNullOrEmpty(GameSession.LastGameScene))
        {
            LoadMainMenu();
            return;
        }

        CancelInvoke();
        SceneManager.LoadScene(GameSession.LastGameScene);
    }

    public void LoadMainMenu()
    {
        CancelInvoke();
        SceneManager.LoadScene(menuScene);
    }
}
