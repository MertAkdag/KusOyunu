using UnityEngine;

[CreateAssetMenu()]
public class PlatformDataSO : ScriptableObject
{
    [SerializeField] Platform[] _platformArray;
    public Platform[] GetPlatformArray => _platformArray;
}
