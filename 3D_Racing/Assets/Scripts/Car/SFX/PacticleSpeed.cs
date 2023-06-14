using UnityEngine;

public class PacticleSpeed : MonoBehaviour, IDependency<Car>
{
    [SerializeField][Range(0.0f, 1.0f)] private float m_baseVolume;

    [SerializeField][Range(0.0f, 50.0f)] private float m_volumeModifier;

    private Car _car;

    private ParticleSystem m_particleSystem;

    public void Construct(Car obj)
    {
        _car = obj;
    }

    private void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    [System.Obsolete]
    private void Update()
    {
        m_particleSystem.startSpeed = m_baseVolume + m_volumeModifier * _car.NormalizedLinearVelocity;

        if (_car.LinearVelocity < 1)
        {
            m_particleSystem.startSpeed = 0;
        }
    }

}
