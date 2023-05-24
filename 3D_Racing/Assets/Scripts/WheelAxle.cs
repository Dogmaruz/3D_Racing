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

    [SerializeField] private bool m_isSteer;

    public void Update()
    {
        SyncMeshTransform();
    }

    public void ApplySteerAngle(float steerAngle)
    {
        if (!m_isSteer) return;

        m_leftWheelCollider.steerAngle = steerAngle;

        m_rightWheelCollider.steerAngle = steerAngle;
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
