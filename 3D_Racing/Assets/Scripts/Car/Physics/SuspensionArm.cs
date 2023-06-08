using UnityEngine;

public class SuspensionArm : MonoBehaviour
{
    [SerializeField] private Transform m_target;

    //[SerializeField] private float m_factor;

    //private float _baseOffset;

    private void Start()
    {
        //_baseOffset = m_target.localPosition.y;
    }

    private void Update()
    {
        //transform.localEulerAngles = new Vector3(0, 0, (m_target.localPosition.y - _baseOffset) * m_factor);

        transform.LookAt(m_target, Vector3.forward);
    }
}
