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