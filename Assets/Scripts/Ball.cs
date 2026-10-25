using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 7;
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
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    public void ResetBall()
    {
        transform.position = startPosition;
        Launch();
    }

    public void Launch()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.Range(-0.7f, 0.7f);

        rb.linearVelocity = new Vector2(x, y).normalized * speed;
    }
}