using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string menuScene = "MainMenu";
    public string playerVsAIScene = "PlayerVSAI";
    public string playerVsPlayerScene = "PlayerVSPlayer";

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            Quit();
    }

    public void PlayVsAI()
    {
        SceneManager.LoadScene(playerVsAIScene);
    }

    public void PlayVsPlayer()
    {
        SceneManager.LoadScene(playerVsPlayerScene);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}