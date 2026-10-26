using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    // Cualquier pala vale: Players para humanos, AI para la máquina
    [SerializeField] private Paddle player1;
    [SerializeField] private Paddle player2;

    [SerializeField] private TMP_Text player1Text;
    [SerializeField] private TMP_Text player2Text;
    [SerializeField] private int maxScore = 3;
    [SerializeField] private string victoryScene = "Victory";

    private int player1Score;
    private int player2Score;
    private bool gameOver;

    void Awake()
    {
        // Avisa en consola de referencias sin asignar en vez de fallar en el primer gol
        if (ball == null || player1 == null || player2 == null || player1Text == null || player2Text == null)
            Debug.LogError("GameManager: faltan referencias por asignar en el Inspector", this);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    // Suma un punto al jugador indicado (1 o 2)
    public void AddScore(int player)
    {
        // Ignora goles una vez hay ganador
        if (gameOver) return;

        if (player == 1)
            player1Score++;
        else
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
        int winner = 0;
        if (player1Score >= maxScore)
            winner = 1;
        else if (player2Score >= maxScore)
            winner = 2;

        if (winner == 0) return false;

        // La escena de victoria lee el ganador de GameSession
        gameOver = true;
        GameSession.Winner = winner;
        GameSession.WinnerIsAI = (winner == 1 ? player1 : player2) is AI;
        SceneManager.LoadScene(victoryScene);
        return true;
    }

}