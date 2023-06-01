﻿using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
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

        UpdateEngineTorque();

        AutoGearShift();

        if (LinearVelocity >= m_maxSpeed) m_engineTorque = 0;

        m_carChassis.MotorTorque = m_engineTorque * ThrottleControl;

        m_carChassis.SteerAngle = m_maxSteerAngle * SteerControl;

        m_carChassis.BrakeTorque = m_maxBrakeTorque * BrakeControl;
    }

    //Gearbox

    private void AutoGearShift()
    {
        if (m_selectedGear < 0) return;

        if (m_engineRPM >= m_upShiftEngineRPM)
        {
            UpGear();
        }

        if (m_engineRPM <= m_downShiftEngineRPM)
        {
            DownGear();
        }
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
    }

    public void SShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNetral()
    {
        m_selectedGear = 0;
    }

    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, m_gears.Length - 1);

        m_selectedGear = m_gears[gearIndex];

        m_selectedGearIndex = gearIndex;
    }

    private void UpdateEngineTorque()
    {
        m_engineRPM = m_engineMinRPM + Mathf.Abs(m_carChassis.GetAverageRPM() * m_selectedGear * m_finalDriveRatio);

        m_engineRPM = Mathf.Clamp(m_engineRPM, m_engineMinRPM, m_engineMaxRPM);

        m_engineTorque = m_engineTorqueCurve.Evaluate(m_engineRPM / m_engineMaxRPM) * m_engineMaxTorque * m_finalDriveRatio * Mathf.Sign(m_selectedGear);
    }
}
