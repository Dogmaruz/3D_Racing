using UnityEngine;

public class UIRaceButtonSpawner : MonoBehaviour
{
    [SerializeField] private Transform m_parent;

    [SerializeField] private UIRaceButton m_prefab;

    [SerializeField] private RaceInfo[] m_properties;

    //private void Awake()
    //{
    //    Spawn();
    //}

    [ContextMenu(nameof(Spawn))]
    public void Spawn()
    {
        //if (Application.isPlaying) return;

        GameObject[] allObject = new GameObject[m_parent.childCount];

        for (int i = 0; i < m_parent.childCount; i++)
        {
            allObject[i] = m_parent.GetChild(i).gameObject;
        }

        for (int i = 0; i < allObject.Length; i++)
        {
            DestroyImmediate(allObject[i]);
        }

        for (int i = 0; i < m_properties.Length; i++)
        {
            UIRaceButton button = Instantiate(m_prefab, m_parent);

            button.ApplyProperty(m_properties[i]);
        }
    }
}
