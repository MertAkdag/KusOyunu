// Copyright (C) 2021 SansDevs.

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _groundCheckRadius;
    [SerializeField] Sprite _idleSprite;
    [SerializeField] Sprite _jumpSprite;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundMask;

    private Rigidbody2D _rb;

    public Vector2 GetPosition => transform.position;

    public static event System.Action OnPlayerDeath;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsGrounded())
        {
            _renderer.sprite = _idleSprite;
        }
        else
        {
            _renderer.sprite = _jumpSprite;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundMask);
    }

    public void Push(Vector2 force)
    {
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ResetVelocity()
    {
        _rb.velocity = Vector2.zero;
    }

    public void Death()
    {
        ResetVelocity();
        gameObject.SetActive(false);

        OnPlayerDeath?.Invoke();
    }
}
