using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPVignette_and_Bloom : MonoBehaviour, IDependency<Car>
{
    private Car _car;

    [SerializeField] [Range(0.0f, 1.0f)] private float m_baseVolume;

    [SerializeField] [Range(0.0f, 20.0f)] private float m_baseVolumeBloom;

    [SerializeField] [Range(0.0f, 1.0f)] private float m_volumeModifier;

    [SerializeField] [Range(0.0f, 20.0f)] private float m_volumeModifierBloom;

    private Volume _ppVolume;

    private Vignette _vignette;

    private Bloom _bloom;

    public void Construct(Car obj)
    {
        _car = obj;
    }

    private void Start()
    {
        _ppVolume = GetComponent<Volume>();

        _ppVolume.profile.TryGet(out _vignette);

        _ppVolume.profile.TryGet(out _bloom);
    }

    private void Update()
    {
        _vignette.intensity.value = m_baseVolume + m_volumeModifier * _car.NormalizedLinearVelocity;

        _bloom.intensity.value = m_baseVolumeBloom + m_volumeModifierBloom * _car.NormalizedLinearVelocity;
    }
}
