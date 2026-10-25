using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public float delay = 3f;
    public string menuScene = "MainMenu";

    void Start()
    {
        Invoke(nameof(LoadMainMenu), delay);
    }

    void Update()
    {

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        CancelInvoke();
        SceneManager.LoadScene(menuScene);
    }
}