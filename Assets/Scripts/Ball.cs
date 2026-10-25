using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    // Componente horizontal mínima de la dirección (0-1) para evitar rebotes casi verticales
    [SerializeField, Range(0f, 0.9f)] private float minHorizontal = 0.3f;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 startPosition;

    void Awake()
    {
        // Si no se asignó en el Inspector, usa el Rigidbody2D del mismo objeto
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

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