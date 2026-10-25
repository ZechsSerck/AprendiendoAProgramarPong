using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Players player1;
    public Players player2;
    public AI aiPlayer;

    public TMP_Text player1Text;
    public TMP_Text player2Text;
    public bool IAGame;
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

        // En modo IA el rival es el componente AI, no Players
        if (IAGame && aiPlayer != null)
            aiPlayer.ResetPosition();
        else if (player2 != null)
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