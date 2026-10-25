using UnityEngine;
using UnityEngine.InputSystem;

public class Players : MonoBehaviour
{
    public bool player1;
    public float speed = 5;
    public Rigidbody2D rb;

    private float move;
    private Vector2 startPosition;

    void Awake()
    {
        startPosition = transform.position;

        // Si la pala la controla la IA, el teclado no debe moverla
        if (GetComponent<AI>() != null)
            enabled = false;
    }

    void Update()
    {
        move = 0;

        var kb = Keyboard.current;
        if (kb == null) return;

        if (player1)
        {
            if (kb.wKey.isPressed) move = 1;
            else if (kb.sKey.isPressed) move = -1;
        }
        else
        {
            if (kb.upArrowKey.isPressed) move = 1;
            else if (kb.downArrowKey.isPressed) move = -1;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, move * speed);
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector2.zero;
        rb.position = startPosition;
    }
}