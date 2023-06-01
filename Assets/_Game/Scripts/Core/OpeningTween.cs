using UnityEngine;

public class OpeningTween : MonoBehaviour
{
    [SerializeField] GameObject _mask;
    [SerializeField] GameObject _bgBlue;
    [SerializeField] float _duration = .5f;
    [SerializeField] float _delayTransition = .2f;
    public LeanTweenType _maskInEase;
    public LeanTweenType _maskOutEase;
    [SerializeField] float _maskOutSize;

    private void OnEnable()
    {
        GameController.Instance.OnGameStarted += PlayTweening;
    }

    public void PlayTweening()
    {
        LeanTween.scale(_mask, Vector3.zero, _duration).setEase(_maskInEase).setOnComplete(PlayMaskOut);
    }

    void PlayMaskOut()
    {
        _bgBlue.SetActive(false);
        LeanTween.scale(_mask, Vector3.one * _maskOutSize, _duration).setEase(_maskOutEase).setDelay(_delayTransition);
    }

    private void Reset()
    {
        _bgBlue.SetActive(true);
        _mask.transform.localScale = Vector3.one;
    }
}
