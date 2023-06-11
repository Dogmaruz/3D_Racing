using System;
using UnityEngine;

public class CarInputControl : MonoBehaviour, IDependency<Car>
{
    private Car _car;

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

    public void Construct(Car obj)
    {
        _car = obj;
    }

    private void Start()
    {
        _previousLinearVelocity = new float[5];
    }

    private void Update()
    {
        _wheelSpeed = _car.WheelSpeed;

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

            _previousLinearVelocity[_car.SelectedGearIndex] = _car.LinearVelocity;

            _car.UpGear();
        }
        //DEBUG
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_verticalAxis < 0) return;

            _car.DownGear();

            _isReducing = true;
        }
    }

    private void ReducingSpeedToPreviousValue()
    {
        if (_car.LinearVelocity > _previousLinearVelocity[_car.SelectedGearIndex])
        {
            _car.BrakeControl = m_brakeCurve.Evaluate(_wheelSpeed / _car.MaxSpeed);

            _car.ThrottleControl = 0;
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
            _car.ThrottleControl = 0;

            _car.BrakeControl = m_handBrakeCurve.Evaluate(_wheelSpeed / _car.MaxSpeed);

            _car.AutoGearShift();
        }
    }

    private void UpdateSteer()
    {
        _car.SteerControl = m_steerCurve.Evaluate(_wheelSpeed / _car.MaxSpeed) * _horizontalAxis;
    }

    private void UpdateThrottleAndBrake()
    {


        if (Mathf.Sign(_verticalAxis) == Mathf.Sign(_wheelSpeed) || Mathf.Abs(_wheelSpeed) < 0.5f)
        {
            _car.ThrottleControl = Mathf.Abs(_verticalAxis);

            _car.BrakeControl = 0;
        }
        else
        {
            _car.ThrottleControl = 0;

            _car.BrakeControl = m_brakeCurve.Evaluate(_wheelSpeed / _car.MaxSpeed);

            _car.AutoGearShift();
        }

        //Gear

        if (_verticalAxis < 0 && _wheelSpeed > -0.5f && _wheelSpeed < 0.5f && _car.LinearVelocity < 1f)
        {
            _car.ShiftToRevertGear();
        }

        if (_verticalAxis > 0 && _wheelSpeed > -0.5f && _wheelSpeed < 0.5f)
        {
            _car.ShiftToFirstGear();
        }

        if (_car.LinearVelocity < 0.1f && _car.LinearVelocity > -0.1f && _verticalAxis == 0)
        {
            _car.ShiftToNetral();
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
            _car.BrakeControl = m_brakeCurve.Evaluate(_wheelSpeed / _car.MaxSpeed) * m_autoBrakeStrength;
        }
    }

    public void Stop()
    {
        Reset();

        _car.BrakeControl = 1;
    }

    public void Reset()
    {
        _verticalAxis = 0;

        _horizontalAxis = 0;

        _handBrakeAxis = 0;

        _car.ThrottleControl = 0;

        _car.SteerControl = 0;

        _car.BrakeControl = 0;
    }
}
