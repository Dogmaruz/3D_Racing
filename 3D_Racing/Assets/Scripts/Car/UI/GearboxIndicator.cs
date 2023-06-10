using UnityEngine;
using UnityEngine.UI;

public class GearboxIndicator : MonoBehaviour, IDependency<Car>
{
    [SerializeField] private Text m_text;

    private Car m_car;

    public void Construct(Car obj)
    {
        m_car = obj;
    }
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
