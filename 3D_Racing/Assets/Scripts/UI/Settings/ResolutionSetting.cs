using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "ResolutionSettings", menuName = "Settings/ResolutionSettings")]
public class ResolutionSetting : Setting
{
    [SerializeField]
    private Vector2Int[] m_avalibaleResolutions = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080)
    };

    private int _currentResolutionIndex = 0;

    public override bool IsMinValue => _currentResolutionIndex == 0;
    public override bool IsMaxValue => _currentResolutionIndex == m_avalibaleResolutions.Length - 1;

    public override void SetNextValue()
    {
        if (!IsMaxValue)
        {
            _currentResolutionIndex++;
        }
    }

    public override void SetPreviousValue()
    {
        if (!IsMinValue)
        {
            _currentResolutionIndex--;
        }
    }

    public override object GetValue()
    {
        return m_avalibaleResolutions[_currentResolutionIndex];
    }

    public override string GetStringValue()
    {
        return m_avalibaleResolutions[_currentResolutionIndex].x + "x" + m_avalibaleResolutions[_currentResolutionIndex].y;
    }

    public override void Apply()
    {
        Screen.SetResolution(m_avalibaleResolutions[_currentResolutionIndex].x, m_avalibaleResolutions[_currentResolutionIndex].y, true);
    }
}
