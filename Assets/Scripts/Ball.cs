using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    // Componente horizontal mínima de la dirección (0-1) para evitar rebotes casi verticales
    [SerializeField, Range(0f, 0.9f)] private float minHorizontal = 0.3f;
    [SerializeField] private Rigidbody2D rb;

    // Ángulo máximo de salida, en grados, al golpear el borde de una pala
    [SerializeField, Range(0f, 70f)] private float maxBounceAngle = 60f;

    // Segundos de espera antes de cada saque
    [SerializeField] private float serveDelay = 1.5f;
    // Texto opcional para mostrar la cuenta atrás
    [SerializeField] private TMP_Text countdownText;

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
        StartCoroutine(ServeAfterDelay());
    }

    void FixedUpdate()
    {
        // Mantiene la rapidez constante después de los rebotes
        if (rb.linearVelocity != Vector2.zero)
            rb.linearVelocity = LimitAngle(rb.linearVelocity.normalized) * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Paddle>() == null) return;

        // Punto de impacto relativo al centro de la pala: -1 (abajo) a 1 (arriba)
        Bounds paddle = collision.collider.bounds;
        float offset = Mathf.Clamp((rb.position.y - paddle.center.y) / paddle.extents.y, -1f, 1f);

        // Sale hacia el lado contrario de la pala, más inclinada cuanto más lejos del centro
        float side = Mathf.Sign(rb.position.x - paddle.center.x);
        float angle = offset * maxBounceAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle) * side, Mathf.Sin(angle));

        rb.linearVelocity = direction * speed;
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
        StopAllCoroutines();
        StartCoroutine(ServeAfterDelay());
    }

    // Deja la bola quieta en el centro, muestra la cuenta atrás y saca
    private IEnumerator ServeAfterDelay()
    {
        rb.linearVelocity = Vector2.zero;

        // Usa tiempo escalado para que la cuenta atrás se detenga en pausa
        float remaining = serveDelay;
        while (remaining > 0)
        {
            if (countdownText != null)
                countdownText.text = Mathf.CeilToInt(remaining).ToString();

            remaining -= Time.deltaTime;
            yield return null;
        }

        if (countdownText != null)
            countdownText.text = "";

        Launch();
    }

    public void Launch()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.Range(-0.7f, 0.7f);

        rb.linearVelocity = new Vector2(x, y).normalized * speed;
    }
}