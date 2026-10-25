using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gameManager;
    public bool isLeftGoal;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        // Un gol en la portería izquierda es punto del jugador 2, y viceversa
        gameManager.AddScore(isLeftGoal ? 2 : 1);
    }
}