using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private WheelCollider[] m_wheelColliders;

    [SerializeField] private Transform[] m_wheelMeshs;

    [SerializeField] private float m_motorTorque;

    [SerializeField] private float m_breakTorque;

    [SerializeField] private float m_steerAngle;


    private void Update()
    {
        for (int i = 0; i < m_wheelColliders.Length; i++)
        {
            m_wheelColliders[i].motorTorque = Input.GetAxis("Vertical") * m_motorTorque;

            m_wheelColliders[i].brakeTorque = Input.GetAxis("Jump") * m_breakTorque;

            Vector3 wheelsPosition;

            Quaternion wheelsRotation;

            m_wheelColliders[i].GetWorldPose(out wheelsPosition, out wheelsRotation);

            m_wheelMeshs[i].position = wheelsPosition;

            m_wheelMeshs[i].rotation = wheelsRotation;
        }

        m_wheelColliders[0].steerAngle = Input.GetAxis("Horizontal") * m_steerAngle;

        m_wheelColliders[1].steerAngle = Input.GetAxis("Horizontal") * m_steerAngle;

        
    }
}
