using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 7;
    // Componente horizontal mínima de la dirección (0-1) para evitar rebotes casi verticales
    [Range(0f, 0.9f)] public float minHorizontal = 0.3f;
    public Rigidbody2D rb;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Launch();
    }

    void FixedUpdate()
    {
        // Mantiene la rapidez constante después de los rebotes
        if (rb.linearVelocity != Vector2.zero)
            rb.linearVelocity = LimitAngle(rb.linearVelocity.normalized) * speed;
    }

    // Si la bola va casi en vertical, endereza la dirección conservando el sentido
    private Vector2 LimitAngle(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) >= minHorizontal)
            return direction;

        float x = Mathf.Sign(direction.x) * minHorizontal;
        float y = Mathf.Sign(direction.y) * Mathf.Sqrt(1f - minHorizontal * minHorizontal);
        return new Vector2(x, y);
    }

    public void ResetBall()
    {
        // Se mueve el Rigidbody2D, no el Transform, para que las físicas no vean un salto
        rb.position = startPosition;
        Launch();
    }

    public void Launch()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.Range(-0.7f, 0.7f);

        rb.linearVelocity = new Vector2(x, y).normalized * speed;
    }
}