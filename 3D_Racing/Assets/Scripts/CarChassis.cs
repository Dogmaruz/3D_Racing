using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent (typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] m_wheelAxles;

    [SerializeField] private float m_wheelBaseLength;

    [SerializeField] private Transform m_centerOfMass;

    [Space(5)]
    [Header("AngularDrag")]
    [SerializeField] private float m_angularDragMin;

    [SerializeField] private float m_angularDragMax;

    [SerializeField] private float m_angularDragFactor;

    [Space(5)]
    [Header("DownForce")]
    [SerializeField] private float m_downForceMin;

    [SerializeField] private float m_downForceMax;

    [SerializeField] private float m_downForceFactor;

    [Space(10)]
    // DEBUG
    public float MotorTorque; //Крутящий момент двигателя.

    public float BrakeTorque; //Сила торможения.

    public float SteerAngle; //Угол поворота колес.

    private Rigidbody _rigidbody;
    public float LinearVelocity => _rigidbody.velocity.magnitude * 3.6f;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_rigidbody != null)
        {//Смещаем центр массы.
            _rigidbody.centerOfMass = m_centerOfMass.localPosition;
        }
    }

    private void FixedUpdate()
    {
        UpdateAngularDrag();

        UpdateDownForce();

        UpdateWheelAxles();
    }

    public float GetAverageRPM()
    {
        float sum = 0;

        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            sum += m_wheelAxles[i].GetAvarageRPM();
        }

        return sum / m_wheelAxles.Length;
    }

    //Получение усредненной скорости колес
    public float GetWheelSpeed()
    {
        return GetAverageRPM() * m_wheelAxles[0].GetRadius() * 2 * 0.1885f;
    }

    //Ограничение угловой силы.
    private void UpdateAngularDrag()
    {
        _rigidbody.angularDrag = Mathf.Clamp(m_angularDragFactor * LinearVelocity, m_angularDragMin, m_angularDragMax);
    }

    //Действие прижимной силы.
    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(m_downForceFactor * LinearVelocity, m_downForceMin, m_downForceMax);

        _rigidbody.AddForce(-transform.up * downForce);
    }

    //Обновление колесных осей.
    private void UpdateWheelAxles()
    {
        int amountMotorWheel = 0;

        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            if (m_wheelAxles[i].IsMotor)
            {
                amountMotorWheel += 2;
            }
        }

        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            m_wheelAxles[i].Update();

            m_wheelAxles[i].ApplyMororTorque(MotorTorque / amountMotorWheel);

            m_wheelAxles[i].ApplySteerAngle(SteerAngle, m_wheelBaseLength);

            m_wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
        }
    }
}
