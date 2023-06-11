using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> OnGearChaged;

    [SerializeField] private float m_maxSteerAngle;

    [SerializeField] private float m_maxBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve m_engineTorqueCurve;

    [SerializeField] private float m_engineMaxTorque;
    //DEBUG
    [SerializeField] private float m_engineTorque;
    //DEBUG
    [SerializeField] private float m_engineRPM;

    [SerializeField] private float m_engineMinRPM;

    [SerializeField] private float m_engineMaxRPM;

    [Header("Gearbox")]
    [SerializeField] private float[] m_gears;

    [SerializeField] private float m_finalDriveRatio;

    [SerializeField] private float m_selectedGear;

    [SerializeField] private float m_rearGear;

    [SerializeField] private float m_upShiftEngineRPM;

    [SerializeField] private float m_downShiftEngineRPM;

    [SerializeField] private int m_selectedGearIndex;
    public int SelectedGearIndex => m_selectedGearIndex;

    [SerializeField] private float m_maxSpeed;

    [SerializeField] private bool m_AutoMode;
    public bool AutoMode => m_AutoMode;

    public float MaxSpeed => m_maxSpeed;
    public float LinearVelocity => m_carChassis.LinearVelocity;
    public float NormalizedLinearVelocity => m_carChassis.LinearVelocity / m_maxSpeed;
    public float WheelSpeed => m_carChassis.GetWheelSpeed();

    public float EngineRPM => m_engineRPM;
    public float EngineMaxRPM => m_engineMaxRPM;

    // DEBUG
    [SerializeField] private float linearVelocity;

    public float ThrottleControl;

    public float SteerControl;

    public float BrakeControl;

    private CarChassis m_carChassis;
    public Rigidbody Rigidbody => m_carChassis == null ? GetComponent<CarChassis>().Rigidbody : m_carChassis.Rigidbody;

    //public float HandBrakeControl;

    private void Start()
    {
        m_carChassis = GetComponent<CarChassis>();

        OnGearChaged?.Invoke(GetSelectedGearName());
    }

    private void Update()
    {
        linearVelocity = LinearVelocity;

        UpdateEngineTorque();

        if (m_AutoMode)
            AutoGearShift();

        if (LinearVelocity >= m_maxSpeed) m_engineTorque = 0;

        m_carChassis.MotorTorque = m_engineTorque * ThrottleControl;

        m_carChassis.SteerAngle = m_maxSteerAngle * SteerControl;

        m_carChassis.BrakeTorque = m_maxBrakeTorque * BrakeControl;
    }

    //Gearbox

    public void AutoGearShift()
    {
        if (m_selectedGear < 0) return;
        if (m_selectedGear == 0) return;

        if (m_engineRPM >= m_upShiftEngineRPM)
        {
            UpGear();
        }

        if (m_engineRPM <= m_downShiftEngineRPM)
        {
            DownGear();
        }
    }

    public string GetSelectedGearName()
    {
        if (m_selectedGear == m_rearGear) return "R";

        if (m_selectedGear == 0) return "N";

        return (m_selectedGearIndex + 1).ToString();
    }

    public void UpGear()
    {
        ShiftGear(m_selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(m_selectedGearIndex - 1);
    }

    public void ShiftToRevertGear()
    {
        m_selectedGear = m_rearGear;

        OnGearChaged?.Invoke(GetSelectedGearName());
    }

    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNetral()
    {
        m_selectedGear = 0;

        OnGearChaged?.Invoke(GetSelectedGearName());
    }

    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, m_gears.Length - 1);

        m_selectedGear = m_gears[gearIndex];

        m_selectedGearIndex = gearIndex;

        OnGearChaged?.Invoke(GetSelectedGearName());
    }

    private void UpdateEngineTorque()
    {
        m_engineRPM = m_engineMinRPM + Mathf.Abs(m_carChassis.GetAverageRPM() * m_selectedGear * m_finalDriveRatio);

        m_engineRPM = Mathf.Clamp(m_engineRPM, m_engineMinRPM, m_engineMaxRPM);

        m_engineTorque = m_engineTorqueCurve.Evaluate(m_engineRPM / m_engineMaxRPM) * m_engineMaxTorque * m_finalDriveRatio * Mathf.Sign(m_selectedGear);
    }

    public void Respawn(Vector3 position, Quaternion rotation)
    {
        Reset();

        transform.position = position;

        transform.rotation = rotation;
    }

    private void Reset()
    {
        m_carChassis.Reset();

        m_carChassis.MotorTorque = 0;

        m_carChassis.BrakeTorque = 0;

        m_carChassis.SteerAngle = 0;

        ThrottleControl = 0;

        BrakeControl = 0;

        SteerControl = 0;
    }
}
