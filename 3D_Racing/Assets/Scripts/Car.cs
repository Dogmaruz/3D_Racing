using UnityEngine;

[RequireComponent (typeof(CarChassis))]
public class Car : MonoBehaviour
{
    [SerializeField] private float m_maxMotorTorque;

    [SerializeField] private float m_maxSteerAngle;

    [SerializeField] private float m_maxBrakeTorque;

    // DEBUG
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
        m_carChassis.MotorTorque = m_maxMotorTorque * ThrottleControl;

        m_carChassis.SteerAngle = m_maxSteerAngle * SteerControl;

        m_carChassis.BrakeTorque = m_maxBrakeTorque * BrakeControl;
    }
}
