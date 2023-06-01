using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("References")]
    [SerializeField] Button _playButton;
    [SerializeField] Button _creditButton;
    [SerializeField] Button _rateButton;
    [SerializeField] Button _settingsButton;

    private void Start()
    {
        OnButtonPressed(_playButton, PlayGame);
        OnButtonPressed(_creditButton, CreditButtonPressed);
        OnButtonPressed(_rateButton, RateButtonPressed);
        OnButtonPressed(_settingsButton, SettingsButtonPressed);
    }

    private void PlayGame()
    {
        GameController.StartGame();
        CloseMenu();
    }

    private void SettingsButtonPressed()
    {
        MenuController.Instance.OpenMenu(MenuType.Setting);
    }

    private void RateButtonPressed()
    {
        MenuController.Instance.OpenMenu(MenuType.Rate);
    }

    private void CreditButtonPressed()
    {
        MenuController.Instance.OpenMenu(MenuType.Credit);
    }
}
