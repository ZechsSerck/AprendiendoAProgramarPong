// Datos que se conservan entre escenas durante la sesión de juego
public enum AIDifficulty { Easy, Normal, Hard }

public static class GameSession
{
    public static AIDifficulty Difficulty = AIDifficulty.Normal;

    // Ganador de la última partida (1 o 2; 0 si aún no hay) y si era la IA
    public static int Winner;
    public static bool WinnerIsAI;
}
