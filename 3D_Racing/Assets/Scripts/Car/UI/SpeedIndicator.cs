using UnityEngine;
using UnityEngine.UI;

public class SpeedIndicator : MonoBehaviour, IDependency<Car>
{
    [SerializeField] private Text m_text;

    private Car _car;

    public void Construct(Car obj)
    {
        _car = obj;
    }

    void Update()
    {
        m_text.text = _car.LinearVelocity.ToString("F0");
    }
}
