using UnityEngine;
using UnityEngine.UI;

public class GearboxIndicator : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] private Text m_text;
    private void Start()
    {
        m_car.OnGearChaged += GearChanged;
    }
    private void GearChanged(string gearName)
    {
        m_text.text = gearName;
    }

    private void OnDestroy()
    {
        m_car.OnGearChaged -= GearChanged;
    }
}
