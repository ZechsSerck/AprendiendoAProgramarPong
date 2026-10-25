using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private string menuScene = "MainMenu";

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