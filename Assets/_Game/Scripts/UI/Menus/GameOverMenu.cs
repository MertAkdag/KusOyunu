using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    [Header("References")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _bestScoreText;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _homeButton;
    [SerializeField] Button _shareButton;

    private ShareOnSocialMedia _share;

    protected override void Awake()
    {
        base.Awake();

        _share = GetComponent<ShareOnSocialMedia>();
    }

    protected override void OnMenuOpened()
    {
        base.OnMenuOpened();

#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlaying) return;
#endif

        SetScoreDisplay();
    }

    private void Start()
    {
        OnButtonPressed(_restartButton, RestartButton);
        OnButtonPressed(_homeButton, HomeButton);
        OnButtonPressed(_shareButton, HandleShareButton);
    }

    private void SetScoreDisplay()
    {
        _scoreText.text = ScoreManager.Instance.Score.ToString();
        _bestScoreText.text = $"Best score: {SaveData.GetBestScore()}";
    }

    private void HandleShareButton()
    {
        _shareButton.interactable = false;

        _share.HandleShare();
    }

    private void HomeButton()
    {
        _homeButton.interactable = false;

        StartCoroutine(ReloadLevelAsync(() =>
        {
            AdsManager.Instance.ShowInterstitial();

            MenuController.Instance.SwitchMenu(MenuType.Main);
            //MenuController.Instance.CloseMenu();
        }));
    }

    private void RestartButton()
    {
        _restartButton.interactable = false;

        StartCoroutine(ReloadLevelAsync(() =>
        {
            AdsManager.Instance.ShowInterstitial();

            MenuController.Instance.CloseMenu();
            GameController.StartGame();
        }));
    }

    IEnumerator ReloadLevelAsync(System.Action OnSceneLoaded = null)
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        OnSceneLoaded?.Invoke();
    }
}
