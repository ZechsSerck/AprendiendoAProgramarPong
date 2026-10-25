using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gameManager;
    public bool isLeftGoal;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        if (isLeftGoal)
            gameManager.Player2Score();
        else
            gameManager.Player1Score();
    }
}