using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : UISelectableButton
{
    [SerializeField] private Setting m_setting;

    [SerializeField] private Text m_titleText;

    [SerializeField] private Text m_valueText;

    [SerializeField] private Image m_previousImage;

    [SerializeField] private Image m_nextImage;

    public void SetNextValueSetting()
    {
        m_setting?.SetNextValue();

        UpdateInfo();

        m_setting?.Apply();
    }

    public void SetPreviousValueSetting()
    {
        m_setting?.SetPreviousValue();

        UpdateInfo();

        m_setting?.Apply();
    }

    private void Start()
    {
        ApplyProperty(m_setting);
    }

    private void UpdateInfo()
    {
        m_titleText.text = m_setting.Title;

        m_valueText.text = m_setting.GetStringValue();

        m_previousImage.enabled = !m_setting.IsMinValue;

        m_nextImage.enabled = !m_setting.IsMaxValue;
    }

    public void ApplyProperty(Setting setting)
    {
        if (setting == null) return;

        m_setting = setting;

        UpdateInfo();

        m_setting?.Apply();
    }
}
