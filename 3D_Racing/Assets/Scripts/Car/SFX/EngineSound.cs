using System;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EngineSound : MonoBehaviour, IDependency<Car>
{
    [SerializeField] private float m_pitchModifier;

    [SerializeField] private float m_volumeModifier;

    [SerializeField] private float m_rpmModifier;

    [SerializeField] private float m_basePitch = 1.0f;

    [SerializeField] private float m_baseVolume = 0.4f;

    [SerializeField] private AudioClip[] m_clips = new AudioClip[7];

    private Car m_car;

    private AudioSource m_engineAudioSourse;

    public void Construct(Car obj)
    {
        m_car = obj;
    }

    private void Start()
    {
        m_car.OnGearChaged += AudioClipGearChanged;

        m_engineAudioSourse = GetComponent<AudioSource> ();

        m_engineAudioSourse.PlayOneShot(m_clips[0]);

        //m_engineAudioSourse.clip = m_clips[1];

        //m_engineAudioSourse.PlayDelayed(0.5f);

    }

    private void Update()
    {
        m_engineAudioSourse.pitch = m_basePitch + m_pitchModifier * ((m_car.EngineRPM / m_car.EngineMaxRPM) * m_rpmModifier);

        m_engineAudioSourse.volume = m_baseVolume + m_volumeModifier * (m_car.EngineRPM / m_car.EngineMaxRPM);
    }

    private void AudioClipGearChanged(string gearName)
    {
        switch (gearName)
        {
            case "R":
                SetAudioClip(m_clips[2]);

                break;
            case "N":
                SetAudioClip(m_clips[1]);

                break;

            default:
                SetAudioClip(m_clips[Int32.Parse(gearName) + 1]);
                
                break;
        }
    }

    private void SetAudioClip (AudioClip clip)
    {
        if (clip == m_engineAudioSourse.clip) return;

        m_engineAudioSourse.clip = clip;

        m_engineAudioSourse.Play();
    }
    private void OnDestroy()
    {
        m_car.OnGearChaged -= AudioClipGearChanged;
    }
}
