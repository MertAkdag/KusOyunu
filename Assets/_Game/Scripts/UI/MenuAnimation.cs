using System.Collections;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] Canvas _canvasHolder;
    [SerializeField] TweenUI[] _objectToAnimate;

    Coroutine disableRoutine;

    public void OpenMenu()
    {
        if (disableRoutine != null)
        {
            StopCoroutine(disableRoutine);
        }

        foreach (var obj in _objectToAnimate)
        {
            obj.HandleOnEnable();
        }
    }

    public void CloseMenu()
    {
        float tweenDuration = 0f;

        foreach (var obj in _objectToAnimate)
        {
            obj.HandleOnDisable();

            // get the longest onDisable tween duration
            float duration = obj.GetOnDisableDuration();
            if (tweenDuration < duration)
                tweenDuration = duration;
        }

        disableRoutine = StartCoroutine(DisableGameobjectRoutine(tweenDuration));
    }

    IEnumerator DisableGameobjectRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        _canvasHolder.enabled = false;
    }
}