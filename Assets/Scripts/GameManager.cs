using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    // Cualquier pala vale: Players para humanos, AI para la máquina
    public Paddle player1;
    public Paddle player2;

    public TMP_Text player1Text;
    public TMP_Text player2Text;
    public int maxScore = 3;

    private int player1Score;
    private int player2Score;
    private bool gameOver;

    void Start()
    {
        UpdateScoreUI();
    }

    public void Player1Score()
    {
        // Ignora goles una vez hay ganador
        if (gameOver) return;

        player1Score++;
        UpdateScoreUI();
        if (!CheckVictory())
            ResetRound();
    }

    public void Player2Score()
    {
        if (gameOver) return;

        player2Score++;
        UpdateScoreUI();
        if (!CheckVictory())
            ResetRound();
    }

    private void UpdateScoreUI()
    {
        player1Text.text = player1Score.ToString();
        player2Text.text = player2Score.ToString();
    }

    private void ResetRound()
    {
        ball.ResetBall();
        player1.ResetPosition();
        player2.ResetPosition();
    }

    // Devuelve true si algún jugador ha ganado y se carga la escena de victoria
    public bool CheckVictory()
    {
        if (player1Score >= maxScore)
        {
            gameOver = true;
            SceneManager.LoadScene("VictoryPlayer1");
        }
        else if (player2Score >= maxScore)
        {
            gameOver = true;
            SceneManager.LoadScene("VictoryPlayer2");
        }

        return gameOver;
    }

}