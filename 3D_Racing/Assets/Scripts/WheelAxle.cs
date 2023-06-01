using System;
using UnityEngine;

[Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider m_leftWheelCollider;

    [SerializeField] private WheelCollider m_rightWheelCollider;

    [SerializeField] private Transform m_leftWheelMesh;

    [SerializeField] private Transform m_rightWheelMesh;

    [SerializeField] private bool m_isMotor;
    public bool IsMotor => m_isMotor;

    [SerializeField] private bool m_isSteer;
    public bool IsSteer => m_isSteer;

    [SerializeField] private float m_wheelWidth;

    [SerializeField] private float m_antiRollForce;

    [SerializeField] private float m_additionalWheelDownForce;

    [SerializeField] private float m_baseForwardStiffnes = 1.5f;

    [SerializeField] private float m_stabilityForvardFactor = 1.0f;

    [SerializeField] private float m_baseSidewaysStiffnes = 2.0f;

    [SerializeField] private float m_stabilitySidewaysFactor = 1.0f;

    private WheelHit _leftWheelHit;

    private WheelHit _rightWheelHit;

    public void Update()
    {
        UpdateWheelHits();

        ApplyAntiRoll();

        ApplyDownForce();

        CorrectStiffness();

        SyncMeshTransform();
    }

    public void ConfigureVehicleSubsteps(float speedThreshold, int speedBelowThreshold, int speedAboveThreshold)
    {
        m_leftWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, speedAboveThreshold);

        m_rightWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, speedAboveThreshold);
    }

    private void UpdateWheelHits()
    {
         m_leftWheelCollider.GetGroundHit(out _leftWheelHit);

         m_rightWheelCollider.GetGroundHit(out _rightWheelHit);
    }

    // Стабилизатор поперечной устойчивости
    private void ApplyAntiRoll()
    {
        float travelLeft = 1.0f;

        float travelRight = 1.0f;

        if (m_leftWheelCollider.isGrounded)
        {
            travelLeft = (-m_leftWheelCollider.transform.InverseTransformPoint(_leftWheelHit.point).y - m_leftWheelCollider.radius) / m_leftWheelCollider.suspensionDistance;
        }

        if (m_rightWheelCollider.isGrounded)
        {
            travelRight = (-m_rightWheelCollider.transform.InverseTransformPoint(_rightWheelHit.point).y - m_rightWheelCollider.radius) / m_rightWheelCollider.suspensionDistance;
        }

        float forceDirection = travelLeft - travelRight;

        if (m_leftWheelCollider.isGrounded) 
        {
            m_leftWheelCollider.attachedRigidbody.AddForceAtPosition(m_leftWheelCollider.transform.up * -forceDirection * m_antiRollForce, m_leftWheelCollider.transform.position);
        }

        if (m_rightWheelCollider.isGrounded)
        {
            m_rightWheelCollider.attachedRigidbody.AddForceAtPosition(m_rightWheelCollider.transform.up * forceDirection * m_antiRollForce, m_rightWheelCollider.transform.position);
        }
    }

    private void ApplyDownForce()
    {
        if (m_leftWheelCollider.isGrounded)
        {
            m_leftWheelCollider.attachedRigidbody.AddForceAtPosition(_leftWheelHit.normal * -m_additionalWheelDownForce * m_leftWheelCollider.attachedRigidbody.velocity.magnitude, m_leftWheelCollider.transform.position);
        }

        if (m_rightWheelCollider.isGrounded)
        {
            m_rightWheelCollider.attachedRigidbody.AddForceAtPosition(_rightWheelHit.normal * -m_additionalWheelDownForce * m_rightWheelCollider.attachedRigidbody.velocity.magnitude, m_rightWheelCollider.transform.position);
        }
    }

    private void CorrectStiffness()
    {
        WheelFrictionCurve leftForward = m_leftWheelCollider.forwardFriction;

        WheelFrictionCurve rightForward = m_rightWheelCollider.forwardFriction;

        WheelFrictionCurve leftSideways = m_leftWheelCollider.sidewaysFriction;

        WheelFrictionCurve rightSideways = m_rightWheelCollider.sidewaysFriction;

        leftForward.stiffness = m_baseForwardStiffnes + Mathf.Abs(_leftWheelHit.forwardSlip) * m_stabilityForvardFactor;

        rightForward.stiffness = m_baseForwardStiffnes + Mathf.Abs(_rightWheelHit.forwardSlip) * m_stabilityForvardFactor;

        leftSideways.stiffness = m_baseSidewaysStiffnes + Mathf.Abs(_leftWheelHit.forwardSlip) * m_stabilitySidewaysFactor;

        rightSideways.stiffness = m_baseSidewaysStiffnes + Mathf.Abs(_rightWheelHit.forwardSlip) * m_stabilitySidewaysFactor;

        m_leftWheelCollider.forwardFriction = leftForward;

        m_rightWheelCollider.forwardFriction = rightForward;

        m_leftWheelCollider.sidewaysFriction = leftSideways;

        m_rightWheelCollider.sidewaysFriction = rightSideways;
    }

    // Применить угол Аккермана
    public void ApplySteerAngle(float steerAngle, float wheelBaseLength)
    {
        if (!m_isSteer) return;

        float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));

        float angleSing = Mathf.Sign(steerAngle);

        if (steerAngle > 0)
        {
            m_leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (m_wheelWidth * 0.5f))) * angleSing;

            m_rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (m_wheelWidth * 0.5f))) * angleSing;
        }
        else if (steerAngle < 0)
        {
            m_leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (m_wheelWidth * 0.5f))) * angleSing;

            m_rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (m_wheelWidth * 0.5f))) * angleSing;
        }
        else
        {
            m_leftWheelCollider.steerAngle = 0;

            m_rightWheelCollider.steerAngle = 0;
        }
    }

    public float GetAvarageRPM()
    {
        return (m_leftWheelCollider.rpm + m_rightWheelCollider.rpm) * 0.5f;
    }

    public float GetRadius()
    {
        return m_leftWheelCollider.radius;
    }

    public void ApplyMororTorque(float motorTorque)
    {
        if (!m_isMotor) return;

        m_leftWheelCollider.motorTorque = motorTorque;

        m_rightWheelCollider.motorTorque = motorTorque;
    }

    public void ApplyBrakeTorque(float brakeTorque)
    {
        m_leftWheelCollider.brakeTorque = brakeTorque;

        m_rightWheelCollider.brakeTorque = brakeTorque;
    }

    private void SyncMeshTransform()
    {
        UpdateWheelTransform(m_leftWheelCollider, m_leftWheelMesh);

        UpdateWheelTransform(m_rightWheelCollider, m_rightWheelMesh);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 wheelsPosition;

        Quaternion wheelsRotation;

        wheelCollider.GetWorldPose(out wheelsPosition, out wheelsRotation);

        wheelTransform.position = wheelsPosition;

        wheelTransform.rotation = wheelsRotation;
    }
}
