using System;
using Extensions;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoSingleton<GameManager>
{
    private static GameState _currentGameState = GameState.Playing;
    private static GameState CurrentGameState
    {
        get => _currentGameState;
        set
        {
            _currentGameState = value;
            OnGameStateChanged?.Invoke(value);
            switch (_currentGameState)
            {
                case GameState.Playing:
                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    break;
            }
        }
    }
    
    public static Action<GameState> OnGameStateChanged;
    
    
    public static void PauseGame()
    {
        if (CurrentGameState == GameState.Playing)
        {
            CurrentGameState = GameState.Paused;
        }
    }
    
    public static void ResumeGame()
    {
        if (CurrentGameState == GameState.Paused)
        {
            CurrentGameState = GameState.Playing;
        }
    }
    
    public static void GameOver()
    {
        CurrentGameState = GameState.GameOver;
    }
}
