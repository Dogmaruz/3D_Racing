using UnityEngine;

public class CameraShaker : CameraComponent
{
    [SerializeField][Range(0.0f, 1.0f)] private float m_normalizeSpeedShake;

    [SerializeField] private float m_shakeAmount;

    private void Update()
    {
        if (m_car.NormalizedLinearVelocity >= m_normalizeSpeedShake)
        {
            transform.localPosition += Random.insideUnitSphere * m_shakeAmount * Time.deltaTime;
        }
    }
}
