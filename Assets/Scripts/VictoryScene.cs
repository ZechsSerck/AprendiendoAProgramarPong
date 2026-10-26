using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
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

        Invoke(nameof(LoadMainMenu), delay);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad < inputDelay) return;

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        CancelInvoke();
        SceneManager.LoadScene(menuScene);
    }
}