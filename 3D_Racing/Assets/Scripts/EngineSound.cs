using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] private float m_pitchModifier;

    [SerializeField] private float m_volumeModifier;

    [SerializeField] private float m_rpmModifier;

    [SerializeField] private float m_basePitch = 1.0f;

    [SerializeField] private float m_baseVolume = 0.4f;

    private AudioSource m_engineAudioSourse;

    private void Start()
    {
        m_engineAudioSourse = GetComponent<AudioSource> ();
    }

    private void Update()
    {
        m_engineAudioSourse.pitch = m_basePitch + m_pitchModifier * ((m_car.EngineRPM / m_car.EngineMaxRPM) * m_rpmModifier);

        m_engineAudioSourse.volume = m_baseVolume + m_volumeModifier * (m_car.EngineRPM / m_car.EngineMaxRPM);
    }
}
