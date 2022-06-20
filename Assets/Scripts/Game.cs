using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : SingletonMonoBehaviour<Game>
{
    public enum GameState
    {
        NotStarted,
        Active,
        GameOver
    }

    [SerializeField] private Bird birdPf;
    [SerializeField] private ObstacleGenerator obstacleGeneratorPf;

    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public GameState CurrentGameState;

    public static event Action<int> OnScoreChanged;
    public static event Action OnGameOver;
    public static event Action OnGameStarted;


    protected override void Awake()
    {
        base.Awake();
        CurrentGameState = GameState.NotStarted;
        HighScore = PlayerPrefs.GetInt("highscore", 0);
    }

    private void Update()
    {
        if (CurrentGameState != GameState.NotStarted)
        {
            return;
        }

        bool isGameStarted = default;
#if UNITY_EDITOR
        isGameStarted = Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame;
#elif UNITY_ANDROID
        isGameStarted = Touchscreen.current.primaryTouch.press.wasPressedThisFrame;
#endif
        if (isGameStarted)
        {
            Instantiate(birdPf);
            Instantiate(obstacleGeneratorPf);
            OnGameStarted?.Invoke();
            CurrentGameState = GameState.Active;
        }
    }

    public void CheckPointEnter()
    {
        Score++;
        OnScoreChanged?.Invoke(Score);
    }

    public void GG()
    {
        if (CurrentGameState == GameState.GameOver) { return; }

        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("highscore", HighScore);
        }

        CurrentGameState = GameState.GameOver;
        OnGameOver?.Invoke();
    }
}