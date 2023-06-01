using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _movingSpeed;
    [SerializeField] [Range(0,1)] private float _paralaxEffect;
    [SerializeField] private Transform _camTransform;
    [SerializeField] private Transform _childTransform;
    [SerializeField] private Sprite _cloudSprite;

    private float _startPos;
    private float _length;

    private void Start()
    {
        _startPos = transform.position.x;
        _length = _cloudSprite.bounds.size.x;
    }

    private void Update()
    {
        float temp = (_camTransform.position.x * (1 - _paralaxEffect));
        float dist = (_camTransform.position.x * _paralaxEffect);

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if (temp > _startPos + _length) _startPos += _length;
        else if (temp < _startPos - _length) _startPos -= _length;

        if (_childTransform == null) return;

        _childTransform.Translate(Vector2.right * Time.deltaTime * _movingSpeed);

        if (_childTransform.localPosition.x < -_length)
        {
            _childTransform.localPosition = Vector2.zero;
        }

    }
}
