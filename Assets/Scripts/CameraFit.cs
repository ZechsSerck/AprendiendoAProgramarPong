using UnityEngine;

// Ajusta la cámara ortográfica para que el campo se vea entero en cualquier relación de aspecto.
// En pantallas más estrechas que targetAspect (por ejemplo 4:3) se aleja; en las más anchas no cambia.
[RequireComponent(typeof(Camera))]
public class CameraFit : MonoBehaviour
{
    [SerializeField] private float targetAspect = 16f / 9f;

    private Camera cam;
    private float baseSize;
    private float lastAspect;

    void Awake()
    {
        cam = GetComponent<Camera>();
        baseSize = cam.orthographicSize;
        Fit();
    }

    void Update()
    {
        // Solo recalcula si cambia la ventana
        if (!Mathf.Approximately(cam.aspect, lastAspect))
            Fit();
    }

    private void Fit()
    {
        lastAspect = cam.aspect;
        cam.orthographicSize = cam.aspect < targetAspect
            ? baseSize * targetAspect / cam.aspect
            : baseSize;
    }
}
