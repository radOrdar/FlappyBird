using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image splashImage;
    [SerializeField] private Image fadeImage;

    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private TextMeshProUGUI gameOverHighScore;
    [SerializeField] private RectTransform gameOverImage;
    [SerializeField] private RectTransform dash;
    [SerializeField] private Button OkButton;

    private void OnEnable()
    {
        Game.OnScoreChanged += HandleOnScoreChanged;
        Game.OnGameOver += HandleGameOver;
        Game.OnGameStarted += HandleOnGameStarted;
    }

    private void OnDisable()
    {
        Game.OnScoreChanged -= HandleOnScoreChanged;
        Game.OnGameOver -= HandleGameOver;
        Game.OnGameStarted -= HandleOnGameStarted;
    }

    private void Start()
    {
        DOTween.Sequence()
            .AppendCallback(() => SetAlphaToImage(fadeImage, 1))
            .Append(fadeImage.DOFade(0, .2f));

        OkButton.onClick.AddListener(() =>
        {
            OkButton.interactable = false;
            DOTween.Sequence()
                .AppendCallback(() => SetAlphaToImage(fadeImage, 0))
                .AppendCallback(() => AudioManager.Instance.PlaySound(AudioManager.ClipName.Swoosh))
                .Append(fadeImage.DOFade(1, .2f))
                .AppendCallback(() => SceneManager.Instance.Restart());
        });
    }

    private void HandleOnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }

    private void HandleOnGameStarted()
    {
        instructions.SetActive(false);
        inGamePanel.SetActive(true);
    }

    private void HandleGameOver()
    {
        inGamePanel.SetActive(false);
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        gameOverScore.text = Game.Instance.Score.ToString();
        gameOverHighScore.text = Game.Instance.HighScore.ToString();
        gameOverPanel.SetActive(true);

        var sequence = DOTween.Sequence();
        sequence.Append(splashImage.DOFade(1, .2f).From()).AppendInterval(.7f)
            .AppendCallback(() => gameOverImage.gameObject.SetActive(true))
            .AppendCallback(() => AudioManager.Instance.PlaySound(AudioManager.ClipName.Swoosh))
            .Append(gameOverImage.DOAnchorPosY(gameOverImage.anchoredPosition.y + 600, .4f).From())
            .Append(dash.DOAnchorPosY(-1500, .4f).From())
            .AppendInterval(.2f).Append(OkButton.GetComponent<Image>().DOFade(1, .5f));
    }

    private void SetAlphaToImage(Image image, float a)
    {
        var fadeInColor = image.color;
        fadeInColor.a = a;
        image.color = fadeInColor;
    }
}