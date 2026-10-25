using UnityEngine;

public class AI : Paddle
{
    public float deadZone = 0.2f;
    public Transform ball;

    void Reset()
    {
        // La IA es más lenta que un jugador por defecto
        speed = 3;
    }

    void Update()
    {
        float difference = ball.position.y - transform.position.y;

        if (Mathf.Abs(difference) > deadZone)
            move = Mathf.Sign(difference);
        else
            move = 0;
    }
}
