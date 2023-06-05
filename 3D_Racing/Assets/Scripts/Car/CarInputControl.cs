using System;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] private AnimationCurve m_brakeCurve;

    [SerializeField] private AnimationCurve m_handBrakeCurve;

    [SerializeField] private AnimationCurve m_steerCurve;

    [SerializeField][Range(0.0f, 1.0f)] private float m_autoBrakeStrength = 0.5f;

    private float _wheelSpeed;

    private float _verticalAxis;

    private float _horizontalAxis;

    private float _handBrakeAxis;

    private float[] _previousLinearVelocity;

    private bool _isReducing;

    private void Start()
    {
        _previousLinearVelocity = new float[5];
    }

    private void Update()
    {
        _wheelSpeed = m_car.WheelSpeed;

        UpdateAxis();

        UpdateThrottleAndBrake();

        UpdateSteer();

        UpdateAutoBrake();

        UpdateHandBrake();

        if (_isReducing)
        {
            ReducingSpeedToPreviousValue();
        }

        //DEBUG
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_verticalAxis < 0) return;

            _previousLinearVelocity[m_car.SelectedGearIndex] = m_car.LinearVelocity;

            m_car.UpGear();
        }
        //DEBUG
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_verticalAxis < 0) return;

            m_car.DownGear();

            _isReducing = true;
        }
    }

    private void ReducingSpeedToPreviousValue()
    {
        if (m_car.LinearVelocity > _previousLinearVelocity[m_car.SelectedGearIndex])
        {
            m_car.BrakeControl = m_brakeCurve.Evaluate(_wheelSpeed / m_car.MaxSpeed);

            m_car.ThrottleControl = 0;
        }
        else
        {
            _isReducing = false;
        }
    }

    private void UpdateHandBrake()
    {
        if (_handBrakeAxis > 0)
        {
            m_car.ThrottleControl = 0;

            m_car.BrakeControl = m_handBrakeCurve.Evaluate(_wheelSpeed / m_car.MaxSpeed);

            m_car.AutoGearShift();
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

            m_car.AutoGearShift();
        }

        //Gear

        if (_verticalAxis < 0 && _wheelSpeed > -0.5f && _wheelSpeed < 0.5f && m_car.LinearVelocity < 1f)
        {
            m_car.ShiftToRevertGear();
        }

        if (_verticalAxis > 0 && _wheelSpeed > -0.5f && _wheelSpeed < 0.5f)
        {
            m_car.ShiftToFirstGear();
        }

        if (m_car.LinearVelocity < 0.1f && m_car.LinearVelocity > -0.1f && _verticalAxis == 0)
        {
            m_car.ShiftToNetral();
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
