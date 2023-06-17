using System;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioMixerSettings", menuName = "Settings/AudioMixerSettings")]
public class AudioMixerFloatSetting : Setting
{
    [SerializeField] private AudioMixer m_audioMixer;

    [SerializeField] private String m_nameParameter;

    [SerializeField] private float m_virtualStep;

    [SerializeField] private float m_minRealValue;

    [SerializeField] private float m_maxRealValue;

    [SerializeField] private float m_minVirtualValue;

    [SerializeField] private float m_maxVirtualValue;

    private float _currentValue = 0;

    public override bool IsMinValue => _currentValue == m_minRealValue;
    public override bool IsMaxValue => _currentValue == m_maxRealValue;

    public override void SetNextValue()
    {
        AddValue(Mathf.Abs(m_maxRealValue - m_minRealValue) / m_virtualStep);
    }

    public override void SetPreviousValue()
    {
        AddValue(-Mathf.Abs(m_maxRealValue - m_minRealValue) / m_virtualStep);
    }

    public override string GetStringValue()
    {
        return Mathf.Lerp(m_minVirtualValue, m_maxVirtualValue, (_currentValue - m_minRealValue) / (m_maxRealValue - m_minRealValue)).ToString();
    }

    public override object GetValue()
    {
        return _currentValue;
    }

    private void AddValue(float value)
    {
        _currentValue += value;

        _currentValue = Mathf.Clamp(_currentValue, m_minRealValue, m_maxRealValue);
    }

    public override void Apply()
    {
        m_audioMixer.SetFloat(m_nameParameter, _currentValue);

        Save();
    }

    public override void Load()
    {
        _currentValue = PlayerPrefs.GetFloat(Title, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(Title, _currentValue);
    }
}
