using UnityEngine;

public enum GameState
{
    InMainMenu,
    Playing,
    Paused,
    GameOver
}

public class GameController : MonoBehaviour
{
    public event System.Action OnGameStarted;
    public event System.Action OnGameOver;

    // Internal variables
    static GameController _instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += GameOver;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= GameOver;
    }

    private void GameOver()
    {
        ScoreManager.Instance.UpdateBestScore();
        Instance.OnGameOver?.Invoke();

        MenuController.Instance.SwitchMenu(MenuType.GameOver);
        SoundManager.Instance.PlayAudio(AudioType.FAIL);
        VibrationManager.Instance.StartVibration();
    }

    public static void StartGame()
    {
        Instance.OnGameStarted?.Invoke();
        MenuController.Instance.OpenMenu(MenuType.Gameplay);
    }

    public static void Quit()
    {
        Application.Quit();
    }

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>(includeInactive: true);
            }
            return _instance;
        }
    }
}
