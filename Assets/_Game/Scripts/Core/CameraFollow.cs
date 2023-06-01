using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _smoothTime = 0.45f;
    [SerializeField] private float _offsetX = 2f;

    private Vector3 _currentVelocity;


    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (Platform.StandingPlatform != null)
        {
            Vector3 targetposition = new Vector3(Platform.StandingPlatform.transform.position.x + _offsetX, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref _currentVelocity, _smoothTime);
        }
    }
}
