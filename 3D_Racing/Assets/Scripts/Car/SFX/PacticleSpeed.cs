using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PacticleSpeed : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField][Range(0.0f, 1.0f)] private float m_baseVolume;

    [SerializeField][Range(0.0f, 50.0f)] private float m_volumeModifier;

    private ParticleSystem m_particleSystem;

    private void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    [System.Obsolete]
    private void Update()
    {
        m_particleSystem.startSpeed = m_baseVolume + m_volumeModifier * m_car.NormalizedLinearVelocity;

        if (m_car.LinearVelocity < 1)
        {
            m_particleSystem.startSpeed = 0;
        }
    }

}
