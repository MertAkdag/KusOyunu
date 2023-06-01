using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : Menu
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] Button _pauseButton;

    private void Start()
    {
        OnButtonPressed(_pauseButton, PauseButtonPressed);
    }

    private void PauseButtonPressed()
    {
        Time.timeScale = 0f;
        MenuController.Instance.OpenMenu(MenuType.Pause);
    }

    private void OnEnable()
    {
        ScoreManager.OnScoreUpdated += UpdateScoreDisplay;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreUpdated -= UpdateScoreDisplay;
    }

    protected override void OnMenuOpened()
    {
        base.OnMenuOpened();

        ResetScoreDisplay();
    }

    private void ResetScoreDisplay()
    {
        _scoreText.text = "0";
    }

    private void UpdateScoreDisplay()
    {
        ScoreManager sm = ScoreManager.Instance;

        if (sm != null)
        {
            _scoreText.text = sm.Score.ToString();
        }
    }
}
