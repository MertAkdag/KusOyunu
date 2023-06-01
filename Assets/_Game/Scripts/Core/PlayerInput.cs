// Copyright (C) 2021 SansDevs.

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] Trajectory _trajectory;
    [SerializeField] Player _player;

    [Space]
    [SerializeField] float pushForce = 4f;

    private bool _firstDrag = true;
    private bool isDragging = false;
    private bool _isFirstDrag;
    private float _distance;

    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 _direction;
    private Vector2 _force;

    public static event System.Action OnFirstDrag;

    private void Start()
    {
        _isFirstDrag = true;
    }

    private void Update()
    {
#if UNITY_EDITOR
        GetInput();
#endif

#if UNITY_ANDROID
        GetMobileInput();
#endif

        if (isDragging)
        {
            OnDrag();
        }
    }

    private void GetMobileInput()
    {
        if (!_player.IsGrounded()) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                isDragging = true;
                OnDragStart();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (!isDragging) return;

                isDragging = false;
                OnDragEnd();
            }
        }
    }

    private void GetInput()
    {
        if (!_player.IsGrounded()) return;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isDragging = true;
            OnDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isDragging) return;

            isDragging = false;
            OnDragEnd();
        }
    }

    private void OnDragStart()
    {
        _startPoint = _cam.ScreenToWorldPoint(Input.mousePosition);

        _trajectory.Show();

        if (!_firstDrag) return;

        _firstDrag = true;
    }

    private void OnDrag()
    {
        _endPoint = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (_startPoint.x <= _endPoint.x)
        {
            _endPoint.x = _startPoint.x;
        }

        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        _force = _direction * _distance * pushForce;

        // just for debug
        Debug.DrawLine(_startPoint, _endPoint);

        _trajectory.UpdateDots(_player.GetPosition, _force);
    }

    private void OnDragEnd()
    {
        if (_isFirstDrag)
        {
            _isFirstDrag = false;
            OnFirstDrag?.Invoke();
        }

        _player.Push(_force);

        _trajectory.Hide();

        SoundManager.Instance.PlayAudio(AudioType.JUMP);
    }
}
