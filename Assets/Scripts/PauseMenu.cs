using UnityEngine;
using UnityEngine.InputSystem;

// Pausa la partida con Esc o P y muestra un panel con Continuar y Menú
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    // Botón "II" en pantalla (para táctil); se oculta mientras el juego está en pausa
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private string menuScene = "MainMenu";

    private bool isPaused;

    void Start()
    {
        ShowPauseUI(false);
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb.escapeKey.wasPressedThisFrame || kb.pKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        ShowPauseUI(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        ShowPauseUI(false);
    }

    // En pausa se ve el panel (Continuar y Menú); jugando, solo el botón "II"
    private void ShowPauseUI(bool paused)
    {
        if (pausePanel != null)
            pausePanel.SetActive(paused);

        if (pauseButton != null)
            pauseButton.SetActive(!paused);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.Load(menuScene);
    }

    void OnDestroy()
    {
        // Evita que la siguiente escena empiece congelada
        Time.timeScale = 1f;
    }
}
