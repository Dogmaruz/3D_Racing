using UnityEngine;

[RequireComponent (typeof(CarChassis))]
public class Car : MonoBehaviour
{
    [SerializeField] private float m_maxMotorTorque;

    [SerializeField] private float m_maxSteerAngle;

    [SerializeField] private float m_maxBrakeTorque;

    [SerializeField] private AnimationCurve m_engineTorqueCurve;

    [SerializeField] private float m_maxSpeed;
    public float MaxSpeed => m_maxSpeed; 

    public float LinearVelocity => m_carChassis.LinearVelocity;
    public float WheelSpeed => m_carChassis.GetWheelSpeed();


    // DEBUG
    [SerializeField] private float linearVelocity;

    public float ThrottleControl;

    public float SteerControl;

    public float BrakeControl;

    private CarChassis m_carChassis;

    //public float HandBrakeControl;

    private void Start()
    {
        m_carChassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        linearVelocity = LinearVelocity;

        float engineTorque = m_engineTorqueCurve.Evaluate(LinearVelocity / m_maxSpeed) * m_maxMotorTorque;

        if (LinearVelocity >= m_maxSpeed) engineTorque = 0;

        m_carChassis.MotorTorque = engineTorque * ThrottleControl;

        m_carChassis.SteerAngle = m_maxSteerAngle * SteerControl;

        m_carChassis.BrakeTorque = m_maxBrakeTorque * BrakeControl;
    }
}
