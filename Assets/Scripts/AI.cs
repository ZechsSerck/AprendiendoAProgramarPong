using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed = 3;
    public float deadZone = 0.2f;
    public Transform ball;
    public Rigidbody2D rb;

    private float move;
    private Vector2 startPosition;

    void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float difference = ball.position.y - transform.position.y;

        if (Mathf.Abs(difference) > deadZone)
            move = Mathf.Sign(difference);
        else
            move = 0;
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