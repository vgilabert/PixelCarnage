using System;
using System.Collections;
using Extensions;
using Managers;
using Player;
using UnityEngine;
using UpgradeSystem;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Upgrading
}

public class GameManager : MonoSingleton<GameManager>
{
    private static GameState _currentGameState = GameState.Playing;
    private static GameState _previousGameState;
    
    private GameState CurrentGameState
    {
        get => _currentGameState;
        set
        {
            _previousGameState = _currentGameState;
            _currentGameState = value;
            OnGameStateChanged?.Invoke(value);
            switch (_currentGameState)
            {
                case GameState.Playing:
                    UIManager.Instance.HidePauseScreen();
                    UIManager.Instance.HideUpgradeScreen();
                    UIManager.Instance.HideGameOverScreen();
                    if (_previousGameState == GameState.Upgrading) // If we were upgrading, we need to increase the timescale back to 1
                    {
                        StartCoroutine(IncreaseTimeScale(0.9f));
                    }
                    else // Set timescale to 1 instantly in other cases
                    {
                        Time.timeScale = 1f;
                    }
                    break;
                
                case GameState.Paused:
                    UIManager.Instance.ShowPauseScreen();
                    UIManager.Instance.HideGameOverScreen();
                    if (_previousGameState == GameState.Playing)
                        Time.timeScale = 0;
                    break;
                
                case GameState.GameOver:
                    UIManager.Instance.ShowGameOverScreen();
                    break;
                
                case GameState.Upgrading:
                    UpgradePhaseManager.Instance.CheckNewDraw();
                    DoAfter(0.9f, () => UIManager.Instance.ShowUpgradeScreen());
                    break;
            }
        }
    }

    private void OnEnable()
    {
        PlayerLeveling.OnLevelUp += OnPlayerLevelUp;
    }
    
    private void OnDisable()
    {
        PlayerLeveling.OnLevelUp -= OnPlayerLevelUp;
    }

    public static Action<GameState> OnGameStateChanged;
    
    private void Start()
    {
        CurrentGameState = GameState.Playing;
        UIManager.Instance.HideUpgradeScreen();
        UIManager.Instance.HidePauseScreen();
        UIManager.Instance.HideGameOverScreen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (CurrentGameState != GameState.Paused)
        {
            CurrentGameState = GameState.Paused;
        }
        else if (CurrentGameState == GameState.Paused)
        {
            CurrentGameState = _previousGameState;
        }
    }
    
    public void GameOver()
    {
        CurrentGameState = GameState.GameOver;
    }

    public void StartUpgradePhase()
    {
        if (CurrentGameState == GameState.Playing)
        {
            CurrentGameState = GameState.Upgrading;
        }
    }
    
    public void EndUpgradePhase()
    {
        if (CurrentGameState == GameState.Upgrading)
        {
            CurrentGameState = GameState.Playing;
        }
    }
    
    private IEnumerator DecreaseTimeScale(float time, Action action = null)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            Time.timeScale = Mathf.Lerp(1, 0, elapsedTime / time);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 0;
        action?.Invoke();
    }
    
    private IEnumerator IncreaseTimeScale(float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            Time.timeScale = Mathf.Lerp(0, 1, elapsedTime / time);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
    }
    
    public void DoAfter(float time, Action action)
    {
        StartCoroutine(DecreaseTimeScale(time, action));
    }
    
    private void OnPlayerLevelUp(int level)
    {
        if (level > 1 && CurrentGameState == GameState.Playing)
        {
            UpgradePhaseManager.Instance.AddDrawCount();
            StartUpgradePhase();
        }
    }
}
