using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedIndicator : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] private Text m_text;
    void Update()
    {
        m_text.text = m_car.LinearVelocity.ToString("F0");
    }
}
