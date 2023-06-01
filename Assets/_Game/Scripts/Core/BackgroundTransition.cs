using UnityEngine;

public class BackgroundTransition : MonoBehaviour
{
    [SerializeField] GameObject _mask;
    [SerializeField] float _duration = .5f;
    [SerializeField] float _delayTransition = .2f;
    public LeanTweenType _maskOutEase;
    [SerializeField] float _maskOutSize;

    private void OnEnable()
    {
        GameController.Instance.OnGameStarted += FadeOut;
        GameController.Instance.OnGameOver += FadeIn;
    }

    void FadeOut()
    {
        LeanTween.scale(_mask, Vector3.one * _maskOutSize, _duration).setEase(_maskOutEase).setDelay(_delayTransition);
    }

    void FadeIn()
    {
        LeanTween.scale(_mask, Vector3.zero, _duration).setEase(_maskOutEase);
    }

    private void Reset()
    {
        _mask.transform.localScale = Vector3.zero;
    }
}
