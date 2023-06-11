using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class EngineIndicatorColor
{
    public float MaxRPM;

    public Color color;
}

public class EngineIndicator : MonoBehaviour, IDependency<Car>
{
    [SerializeField] private Image m_image;

    [SerializeField] private EngineIndicatorColor[] m_colors;

    private Car _car;

    public void Construct(Car obj)
    {
        _car = obj;
    }

    void Update()
    {
        m_image.fillAmount = _car.EngineRPM / _car.EngineMaxRPM;

        for (int i = 0; i < m_colors.Length; i++)
        {
            if (_car.EngineRPM <= m_colors[i].MaxRPM)
            {
                m_image.color = m_colors[i].color;

                break;
            }
        }
    }
}
