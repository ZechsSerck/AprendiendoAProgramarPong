using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string menuScene = "MainMenu";
    [SerializeField] private string playerVsAIScene = "PlayerVSAI";
    [SerializeField] private string playerVsPlayerScene = "PlayerVSPlayer";

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            Quit();
    }

    // Juega contra la IA con la última dificultad elegida (Normal por defecto)
    public void PlayVsAI()
    {
        SceneManager.LoadScene(playerVsAIScene);
    }

    public void PlayVsAIEasy()
    {
        GameSession.Difficulty = AIDifficulty.Easy;
        PlayVsAI();
    }

    public void PlayVsAINormal()
    {
        GameSession.Difficulty = AIDifficulty.Normal;
        PlayVsAI();
    }

    public void PlayVsAIHard()
    {
        GameSession.Difficulty = AIDifficulty.Hard;
        PlayVsAI();
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