using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private string menuScene = "MainMenu";
    // Segundos en los que se ignora el teclado, para no saltar la pantalla sin querer
    [SerializeField] private float inputDelay = 0.5f;

    void Start()
    {
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