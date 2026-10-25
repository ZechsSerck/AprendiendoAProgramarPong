using UnityEngine;
using UnityEngine.InputSystem;

public class Players : Paddle
{
    public bool player1;

    protected override void Awake()
    {
        base.Awake();

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
}
