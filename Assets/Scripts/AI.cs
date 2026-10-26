using UnityEngine;

public class AI : Paddle
{
    [System.Serializable]
    public struct DifficultySettings
    {
        public float speed;
        public float deadZone;
        // Segundos entre cada vez que la IA vuelve a mirar la bola
        public float reactionTime;
        // Error máximo, en unidades, al calcular a qué altura ir
        public float aimError;
    }

    [SerializeField] private Transform ball;

    // La velocidad de la IA sale de estos ajustes según la dificultad elegida en el menú
    [SerializeField] private DifficultySettings easy = new DifficultySettings { speed = 2.2f, deadZone = 0.3f, reactionTime = 0.3f, aimError = 0.8f };
    [SerializeField] private DifficultySettings normal = new DifficultySettings { speed = 3f, deadZone = 0.2f, reactionTime = 0.1f, aimError = 0.3f };
    [SerializeField] private DifficultySettings hard = new DifficultySettings { speed = 4.5f, deadZone = 0.1f, reactionTime = 0.03f, aimError = 0.1f };

    private DifficultySettings settings;
    private float targetY;
    private float nextReactionTime;

    protected override void Awake()
    {
        base.Awake();

        if (ball == null)
            Debug.LogError("AI: falta asignar la bola en el Inspector", this);

        settings = GameSession.Difficulty switch
        {
            AIDifficulty.Easy => easy,
            AIDifficulty.Hard => hard,
            _ => normal,
        };
        speed = settings.speed;
        targetY = transform.position.y;
    }

    void Update()
    {
        if (ball == null) return;

        // Solo actualiza el objetivo cada reactionTime, con algo de error
        if (Time.time >= nextReactionTime)
        {
            nextReactionTime = Time.time + settings.reactionTime;
            targetY = ball.position.y + Random.Range(-settings.aimError, settings.aimError);
        }

        float difference = targetY - transform.position.y;

        if (Mathf.Abs(difference) > settings.deadZone)
            move = Mathf.Sign(difference);
        else
            move = 0;
    }
}
