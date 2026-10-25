using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Players player1;
    public Players player2;

    public TMP_Text player1Text;
    public TMP_Text player2Text;
    public bool IAGame;
    public int maxScore = 3;

    private int player1Score;
    private int player2Score;

    void Start()
    {
        UpdateScoreUI();
    }

    public void Player1Score()
    {
        player1Score++;
        UpdateScoreUI();
        CheckVictory();
        ResetRound();
    }

    public void Player2Score()
    {
        player2Score++;
        UpdateScoreUI();
        CheckVictory();
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

    public void CheckVictory()
    {
        if (player1Score >= maxScore)
        {
            SceneManager.LoadScene("VictoryPlayer1");
        }

        if (player2Score >= maxScore)
        {
            SceneManager.LoadScene("VictoryPlayer2");
        }
    }

}