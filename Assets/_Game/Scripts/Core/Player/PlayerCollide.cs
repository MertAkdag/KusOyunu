using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            if (_player != null)
            {
                _player.Death();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (_player != null)
            {
                _player.ResetVelocity();
            }
        }
    }
}
