using UnityEngine;

// Base común de las palas: movimiento vertical y reset de posición
public abstract class Paddle : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;

    // Dirección vertical (-1, 0 o 1) que decide cada tipo de pala en Update
    protected float move;

    private Vector2 startPosition;

    protected virtual void Awake()
    {
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
