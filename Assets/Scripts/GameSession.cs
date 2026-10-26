// Datos que se conservan entre escenas durante la sesión de juego
public enum AIDifficulty { Easy, Normal, Hard }

public static class GameSession
{
    public static AIDifficulty Difficulty = AIDifficulty.Normal;
}
