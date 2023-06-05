using UnityEngine;

public class WindSound : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] private float m_basePitch = 1.0f;

    [SerializeField] private float m_baseVolume = 0.4f;

    [SerializeField] private float m_pitchModifier;

    [SerializeField] private float m_volumeModifier;

    [SerializeField] private float m_rpmModifier;

    private AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        m_audioSource.pitch = m_basePitch + m_pitchModifier * (m_car.NormalizedLinearVelocity * m_rpmModifier);

        m_audioSource.volume = m_baseVolume + m_volumeModifier * m_car.NormalizedLinearVelocity;
    }
}
