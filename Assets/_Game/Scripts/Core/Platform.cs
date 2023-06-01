using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static Platform StandingPlatform;


    [Space]
    [SerializeField] bool _isMovingHorizontal;
    [SerializeField] float _horizontalMoveLimit;
    [SerializeField] float _horizontalMoveSpeed;

    [Space]
    [SerializeField] bool _isMovingVertical;
    [SerializeField] float _verticalMoveLimit;
    [SerializeField] float _verticalMoveSpeed;

    [Space]
    [SerializeField] bool _canFall;
    [SerializeField] float _fallingDelay;
    [SerializeField] float _fallingSpeed;
    [SerializeField] float _fallingVibrationStrength = .05f;

    private int _horizontalMoveVal = 1;
    private int _verticalMoveVal = 1;
    private bool _isFalling;

    private Vector2 _initialPosition;

    public static event System.Action OnPlayerLanding;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (_isFalling)
        {
            transform.Translate(Vector2.down * Time.deltaTime * _fallingSpeed);
        }

        HorizontalMovement(transform);
        VerticalMovement(transform);
    }

    private void HorizontalMovement(Transform target)
    {
        if (target.position.x > _horizontalMoveLimit + _initialPosition.x)
        {
            _horizontalMoveVal = -1;
        }
        else if (target.position.x < -_horizontalMoveLimit + _initialPosition.x)
        {
            _horizontalMoveVal = 1;
        }

        if (_isMovingHorizontal)
        {
            if (StandingPlatform == this) return;

            target.Translate(Vector2.right * Time.deltaTime * _horizontalMoveVal * _horizontalMoveSpeed);
        }
    }

    private void VerticalMovement(Transform target)
    {
        if (target.position.y > _verticalMoveLimit + _initialPosition.y)
        {
            _verticalMoveVal = -1;
        }
        else if (target.position.y < -_verticalMoveLimit + _initialPosition.y)
        {
            _verticalMoveVal = 1;
        }

        if (_isMovingVertical)
        {
            if (StandingPlatform == this) return;

            target.Translate(Vector2.up * Time.deltaTime * _verticalMoveVal * _verticalMoveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null && !player.IsGrounded()) return;

            if (StandingPlatform != this)
            {
                StandingPlatform = this;
                OnPlayerLanding?.Invoke();

                ScoreManager.Instance.AddScore(1);
                SoundManager.Instance.PlayAudio(AudioType.SCORE);
            }

            if (_canFall)
            {
                _canFall = false;
                StartCoroutine(FallingPlatformRoutine());
            }
        }
    }

    IEnumerator FallingPlatformRoutine()
    {
        yield return new WaitForSeconds(_fallingDelay);
        _isFalling = true;

        int temp = 1;

        while (true)
        {
            transform.position += Vector3.right * _fallingVibrationStrength * temp;
            temp *= -1;

            yield return new WaitForSeconds(.05f);
        }
    }
}
