using System;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] private AnimationCurve m_brakeCurve;

    [SerializeField] private AnimationCurve m_steerCurve;

    [SerializeField] [Range(0.0f, 1.0f)] private float  m_autoBrakeStrength = 0.5f;

    private float _wheelSpeed;

    private float _verticalAxis;

    private float _horizontalAxis;

    private float _handBrakeAxis;

    private void Update()
    {
        _wheelSpeed = m_car.WheelSpeed;

        UpdateAxis();

        UpdateThrottleAndBrake();

        UpdateSteer();

        UpdateAutoBrake();

        UpdateHandBrake();

        //DEBUG
        if (Input.GetKeyDown(KeyCode.E))
            m_car.UpGear();
        //DEBUG
        if (Input.GetKeyDown(KeyCode.Q))
            m_car.DownGear();
    }

    private void UpdateHandBrake()
    {
        if (_handBrakeAxis > 0)
        {
            m_car.BrakeControl = 1;

            m_car.ThrottleControl = 0;
        }
    }

    private void UpdateSteer()
    {
        m_car.SteerControl = m_steerCurve.Evaluate(_wheelSpeed / m_car.MaxSpeed) * _horizontalAxis;
    }

    private void UpdateThrottleAndBrake()
    {
        if (Mathf.Sign(_verticalAxis) == Mathf.Sign(_wheelSpeed) || Mathf.Abs(_wheelSpeed) < 0.5f)
        {
            m_car.ThrottleControl = Mathf.Abs(_verticalAxis);

            m_car.BrakeControl = 0;
        }
        else
        {
            m_car.ThrottleControl = 0;

            m_car.BrakeControl = m_brakeCurve.Evaluate(_wheelSpeed / m_car.MaxSpeed);
        }

        //Gear

        if (_verticalAxis < 0 && _wheelSpeed > -0.5f && _wheelSpeed <= 0.5f)
        {
            m_car.ShiftToRevertGear();
        }

        if (_verticalAxis > 0 && _wheelSpeed > -0.5f && _wheelSpeed < 0.5f)
        {
            m_car.SShiftToFirstGear();
        }
    }

    private void UpdateAxis()
    {
        _verticalAxis = Input.GetAxis("Vertical");

        _horizontalAxis = Input.GetAxis("Horizontal");

        _handBrakeAxis = Input.GetAxis("Jump");
    }

    private void UpdateAutoBrake()
    {
        if (_verticalAxis == 0)
        {
            m_car.BrakeControl = m_brakeCurve.Evaluate(_wheelSpeed / m_car.MaxSpeed) * m_autoBrakeStrength;
        }
    }
}
