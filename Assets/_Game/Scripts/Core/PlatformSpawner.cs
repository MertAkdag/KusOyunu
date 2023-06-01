using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner Instance => _instance;
    private static PlatformSpawner _instance;

    [SerializeField] Transform _player;
    [SerializeField] float _gap = 8f;

    [Space]
    [SerializeField] PlatformDataSO _platformData;

    Vector3 _lastSpawnPosition;
    int _platformCount;
    Platform _selectedPlatform;

    private void OnEnable()
    {
        Platform.OnPlayerLanding += SpawnPlatform;
    }

    private void OnDisable()
    {
        Platform.OnPlayerLanding -= SpawnPlatform;
    }

    private void Start()
    {
        _instance = this;

        _lastSpawnPosition = Vector3.right * _gap;
        _lastSpawnPosition.x += _player.position.x;

        CalculateDifficulty();

        SpawnPlatform();
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private void SpawnPlatform()
    {
        CalculateDifficulty();

        Transform newPlatform = Instantiate(_selectedPlatform.transform, transform);
        newPlatform.position = _lastSpawnPosition;

        _lastSpawnPosition += Vector3.right * _gap;
        _lastSpawnPosition.y = Random.Range(-1.5f, 1.5f);

        _platformCount++;
    }

    void CalculateDifficulty()
    {
        if (_platformCount < 3)
        {
            _selectedPlatform = _platformData.GetPlatformArray[Random.Range(0, 2)];
        }
        else if (_platformCount >= 3 && _platformCount < 10)
        {
            _selectedPlatform = _platformData.GetPlatformArray[Random.Range(0, 5)];
        }
        else if (_platformCount >= 10 && _platformCount < 15)
        {
            _selectedPlatform = _platformData.GetPlatformArray[Random.Range(0, 9)];
        }
        else if (_platformCount >= 15)
        {
            _selectedPlatform = _platformData.GetPlatformArray[Random.Range(0, 14)];
        }
    }
}
