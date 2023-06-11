using UnityEngine;
using UnityEngine.UI;

public class GearboxIndicator : MonoBehaviour, IDependency<Car>
{
    [SerializeField] private Text m_text;

    private Car _car;

    public void Construct(Car obj)
    {
        _car = obj;
    }
    private void Start()
    {
        _car.OnGearChaged += GearChanged;
    }
    private void GearChanged(string gearName)
    {
        m_text.text = gearName;
    }

    private void OnDestroy()
    {
        _car.OnGearChaged -= GearChanged;
    }
}
