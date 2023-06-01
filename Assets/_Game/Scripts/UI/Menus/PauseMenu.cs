using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _resumeButton;
    [SerializeField] private Button _toggleMusicButton;
    [SerializeField] private Button _toggleSFXButton;
    [SerializeField] private Button _toggleVibrateButton;
    [SerializeField] private Button _homeButton;

    [Header("SFX Image Toggle")]
    [SerializeField] Sprite _sfxTrue;
    [SerializeField] Sprite _sfxFalse;

    [Header("Music Image Toggle")]
    [SerializeField] Sprite _musicTrue;
    [SerializeField] Sprite _musicFalse;

    [Header("Vibrate Image Toggle")]
    [SerializeField] Sprite _vibrateTrue;
    [SerializeField] Sprite _vibrateFalse;

    private Image _musicImage;
    private Image _sfxImage;
    private Image _vibrateImage;

    protected override void OnMenuOpened()
    {
        base.OnMenuOpened();

        _resumeButton.interactable = true;
        _homeButton.interactable = true;

        SetIconToggle();
    }

    private void Start()
    {
        _musicImage = _toggleMusicButton.GetComponent<Image>();
        _sfxImage = _toggleSFXButton.GetComponent<Image>();
        _vibrateImage = _toggleVibrateButton.GetComponent<Image>();

        OnButtonPressed(_resumeButton, ResumeButtonPressed);

        OnButtonPressed(_toggleMusicButton, ToggleMusicButtonListener);
        OnButtonPressed(_toggleSFXButton, ToggleSFXButtonListener);
        OnButtonPressed(_toggleVibrateButton, ToggleVibrateButtonListener);
        OnButtonPressed(_homeButton, HomeButtonListener);

        SetIconToggle();
    }

    private void HomeButtonListener()
    {
        Time.timeScale = 1f;
        _homeButton.interactable = false;

        StartCoroutine(ReloadLevelAsync(() =>
        {
            MenuController.Instance.CloseMenu();
            MenuController.Instance.SwitchMenu(MenuType.Main);
        }));
    }

    private void SetIconToggle()
    {
        _musicImage.sprite = SaveData.GetMusicState() ? _musicTrue : _musicFalse;
        _sfxImage.sprite = SaveData.GetSfxState() ? _sfxTrue : _sfxFalse;
        _vibrateImage.sprite = SaveData.GetVibrateState() ? _vibrateTrue : _vibrateFalse;
    }

    private void ToggleMusicButtonListener()
    {
        SoundManager.Instance.ToggleMusic();
        _musicImage.sprite = SaveData.GetMusicState() ? _musicTrue : _musicFalse;
    }

    private void ToggleSFXButtonListener()
    {
        SoundManager.Instance.ToggleFX();
        _sfxImage.sprite = SaveData.GetSfxState() ? _sfxTrue : _sfxFalse;
    }

    private void ToggleVibrateButtonListener()
    {
        VibrationManager.Instance.ToggleVibration();
        _vibrateImage.sprite = SaveData.GetVibrateState() ? _vibrateTrue : _vibrateFalse;

        VibrationManager.Instance.StartVibration();
    }

    private void ResumeButtonPressed()
    {
        Time.timeScale = 1f;

        _resumeButton.interactable = false;

        MenuController.Instance.CloseMenu();
    }

    IEnumerator ReloadLevelAsync(System.Action OnSceneLoaded = null)
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        OnSceneLoaded?.Invoke();
    }
}