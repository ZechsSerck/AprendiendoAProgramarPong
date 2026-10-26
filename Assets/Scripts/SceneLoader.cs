using UnityEngine;
using UnityEngine.SceneManagement;

// Punto único para cambiar de escena: avisa si el nombre no existe en la build
public static class SceneLoader
{
    public static bool Load(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName) || !Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"SceneLoader: la escena \"{sceneName}\" no existe o no está en la lista de la build");
            return false;
        }

        SceneManager.LoadScene(sceneName);
        return true;
    }
}
