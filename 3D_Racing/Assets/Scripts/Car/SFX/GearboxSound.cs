using System;
using UnityEngine;

public class GearboxSound : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] private AudioClip m_clip;

    private AudioSource _engineAudioSourse;

    private string _currentGearName;


    private void Start()
    {
        m_car.OnGearChaged += GearChanged;

        _engineAudioSourse = GetComponent<AudioSource>();
    }

    private void GearChanged(string gearName)
    {
        if (gearName == _currentGearName) return;
        else
            _currentGearName = gearName;

        _engineAudioSourse.PlayOneShot(m_clip);
    }

    private void OnDestroy()
    {
        m_car.OnGearChaged -= GearChanged;
    }
}
