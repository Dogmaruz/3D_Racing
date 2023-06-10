using UnityEngine;
using UnityEngine.UI;

public class SpeedIndicator : MonoBehaviour, IDependency<Car>
{
    [SerializeField] private Text m_text;

    private Car m_car;

    public void Construct(Car obj)
    {
        m_car = obj;
    }

    void Update()
    {
        m_text.text = m_car.LinearVelocity.ToString("F0");
    }
}
