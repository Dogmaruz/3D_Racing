using UnityEngine;

[CreateAssetMenu(fileName = "RaceInfo", menuName = "Race/RaceInfo")]
public class RaceInfo : ScriptableObject
{
    [SerializeField] private string m_sceneName;
    public string SceneName => m_sceneName;

    [SerializeField] private Sprite m_icon;
    public Sprite Icon => m_icon;

    [SerializeField] private string m_title;
    public string Title => m_title;
}
