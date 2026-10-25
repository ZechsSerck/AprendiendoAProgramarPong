using UnityEngine;

// Base común de las palas: movimiento vertical y reset de posición
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Paddle : MonoBehaviour
{
    [SerializeField] protected float speed = 5;
    [SerializeField] private Rigidbody2D rb;

    // Dirección vertical (-1, 0 o 1) que decide cada tipo de pala en Update
    protected float move;

    private Vector2 startPosition;

    protected virtual void Awake()
    {
        // Si no se asignó en el Inspector, usa el Rigidbody2D del mismo objeto
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, move * speed);
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector2.zero;
        rb.position = startPosition;
    }
}
