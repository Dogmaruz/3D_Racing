using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private RaceInfo m_raceInfo;

    [SerializeField] private Image m_icon;

    [SerializeField] private Text m_title;

    [SerializeField] private GameObject m_lockImage;

    private bool _isCompleted;

    private string _sceneName;
    public string SceneName => _sceneName;

    public RaceInfo RaceInfo => m_raceInfo;

    private void Awake()
    {
        ApplyProperty(m_raceInfo);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (m_raceInfo == null) return;

        if (m_lockImage.activeSelf == true) return;

        SceneManager.LoadScene(m_raceInfo.SceneName);
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;

        if (property is RaceInfo == false) return;

        m_raceInfo = property as RaceInfo;

        m_icon.sprite = m_raceInfo.Icon;

        m_title.text = m_raceInfo.Title;

        _sceneName = m_raceInfo.SceneName;
    }

    public bool GetRaceState()
    {
        return _isCompleted;
    }

    public void SetRaceState(bool isCompleted)
    {
        _isCompleted = isCompleted;
    }

    public void SetLockImageVisable(bool isLook)
    {
        m_lockImage.SetActive(isLook);
    }
}
