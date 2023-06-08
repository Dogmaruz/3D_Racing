using UnityEngine;

public class WheelEffect : MonoBehaviour
{
    [SerializeField] private WheelCollider[] m_wheels;

    [SerializeField] private ParticleSystem[] m_wheelsSmoke;

    [SerializeField] private float m_forwardSlipLimit;

    [SerializeField] private float m_sidewaySlipLimit;

    [SerializeField] private AudioSource m_skidAudio;

    [SerializeField] private GameObject m_skidPrafab;

    private WheelHit _wheelHit;

    private Transform[] _skidTrail;

    private void Start()
    {
        _skidTrail = new Transform[m_wheels.Length];
    }

    private void Update()
    {
        bool isSlip = false;

        for (int i = 0; i < m_wheels.Length; i++)
        {
            m_wheels[i].GetGroundHit(out _wheelHit);

            if (m_wheels[i].isGrounded)
            {
                if (_wheelHit.forwardSlip > m_forwardSlipLimit || _wheelHit.sidewaysSlip > m_sidewaySlipLimit || _wheelHit.forwardSlip < -m_forwardSlipLimit || _wheelHit.sidewaysSlip < -m_sidewaySlipLimit)
                {
                    if (_skidTrail[i] == null)
                    {
                        _skidTrail[i] = Instantiate(m_skidPrafab).transform;
                    }

                    if (!m_skidAudio.isPlaying)
                    {
                        m_skidAudio.Play();
                    }

                    if (_skidTrail[i] != null)
                    {
                        _skidTrail[i].position = m_wheels[i].transform.position - _wheelHit.normal * m_wheels[i].radius;

                        _skidTrail[i].forward = -_wheelHit.normal;

                        m_wheelsSmoke[i].transform.position = _skidTrail[i].position;

                        m_wheelsSmoke[i].Emit(1);
                    }

                    isSlip = true;

                    continue;
                }
            }

            _skidTrail[i] = null;

            m_wheelsSmoke[i].Stop();
        }

        if (!isSlip)
        {
            m_skidAudio.Stop();
        }
    }
}
