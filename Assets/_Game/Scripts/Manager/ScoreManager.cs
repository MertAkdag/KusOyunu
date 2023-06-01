using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance => _instance;
    private static ScoreManager _instance;

    public int Score => _score;
    private int _score;

    public static event System.Action OnScoreUpdated;

    private void Awake()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    public void AddScore(int scoreVal)
    {
        _score += scoreVal;

        OnScoreUpdated?.Invoke();
    }

    public void UpdateBestScore()
    {
        int bestScore;

        bestScore = SaveData.GetBestScore();
        if (_score > bestScore)
        {
            SaveData.SetBestScore(_score);
        }
    }
}
