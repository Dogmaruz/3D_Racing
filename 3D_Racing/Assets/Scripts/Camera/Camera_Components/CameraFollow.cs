using UnityEngine;

public class CameraFollow : CameraComponent
{
    [Header("Offset")]
    [SerializeField] private float m_viewHeight;

    [SerializeField] private float m_height;

    [SerializeField] private float m_distance;

    [Header("Damping")]
    [SerializeField] private float m_rotationDamping;

    [SerializeField] private float m_heightDamping;

    [SerializeField] private float m_speedThreshold;

    private Transform _target;

    private Rigidbody _rigidbody;

    private void FixedUpdate()
    {
        Vector3 velocity = _rigidbody.velocity;

        Vector3 targetRotation = _target.eulerAngles;

        if (velocity.magnitude > m_speedThreshold)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
        }

        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, m_rotationDamping * Time.fixedDeltaTime);

        float currentHeight = Mathf.Lerp(transform.position.y, _target.position.y + m_height, m_heightDamping * Time.fixedDeltaTime);

        Vector3 positionOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * m_distance;

        transform.position = _target.position - positionOffset;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        transform.LookAt(_target.position + new Vector3(0, m_viewHeight, 0));
    }

    public override void SetProperties(Car car, Camera camera)
    {
        base.SetProperties(car, camera);

        _target = car.transform;

        _rigidbody = car.Rigidbody;
    }
}
