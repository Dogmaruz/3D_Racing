using UnityEngine;

public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] m_wheelAxles;

    // DEBUG
    public float MotorTorque;

    public float BrakeTorque;

    public float SteerAngle;

    private void FixedUpdate()
    {
        UpdateWheelAxles();
    }

    private void UpdateWheelAxles()
    {
        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            m_wheelAxles[i].Update();

            m_wheelAxles[i].ApplyMororTorque(MotorTorque);

            m_wheelAxles[i].ApplySteerAngle(SteerAngle);

            m_wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
        }
    }
}
