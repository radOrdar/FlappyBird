using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : SingletonMonoBehaviour<Game>
{
    [SerializeField] private Bird birdPf;
    [SerializeField] private ObstacleGenerator obstacleGeneratorPf;

    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public bool IsGameOver { get; private set; }

    public static event Action<int> OnScoreChanged;
    public static event Action OnGameOver;
    public static event Action OnGameStarted;

    private bool isGameStarted;

    protected override void Awake()
    {
        base.Awake();
        HighScore = PlayerPrefs.GetInt("highscore", 0);
    }

    private void Update()
    {
        if (isGameStarted)
        {
            return;
        }
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
        }
    }

    public void CheckPointEnter()
    {
        Score++;
        OnScoreChanged?.Invoke(Score);
    }

    public void GG()
    {
        if (IsGameOver) { return; }

        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("highscore", HighScore);
        }

        IsGameOver = true;
        OnGameOver?.Invoke();
    }
}