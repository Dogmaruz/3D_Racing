using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIRaceButton : UISelectableButton
{
    [SerializeField] private RaceInfo m_raceInfo;

    [SerializeField] private Image m_icon;

    [SerializeField] private Text m_title;

    private void Start()
    {
        ApplyProperty(m_raceInfo);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (m_raceInfo == null) return;

        SceneManager.LoadScene(m_raceInfo.SceneName);
    }

    public void ApplyProperty(RaceInfo property)
    {
        if (property == null) return;

        m_raceInfo = property;

        m_icon.sprite = m_raceInfo.Icon;

        m_title.text = m_raceInfo.Title;
    }
}
