using UnityEngine;

public class AI : Paddle
{
    [SerializeField] private float deadZone = 0.2f;
    [SerializeField] private Transform ball;

    void Reset()
    {
        // La IA es más lenta que un jugador por defecto
        speed = 3;
    }

    protected override void Awake()
    {
        base.Awake();

        if (ball == null)
            Debug.LogError("AI: falta asignar la bola en el Inspector", this);
    }

    void Update()
    {
        if (ball == null) return;

        float difference = ball.position.y - transform.position.y;

        if (Mathf.Abs(difference) > deadZone)
            move = Mathf.Sign(difference);
        else
            move = 0;
    }
}
