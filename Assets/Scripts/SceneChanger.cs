using UnityEngine;
using UnityEngine.InputSystem;

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
        SceneLoader.Load(playerVsAIScene);
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
        SceneLoader.Load(playerVsPlayerScene);
    }

    public void GoToMenu()
    {
        SceneLoader.Load(menuScene);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}