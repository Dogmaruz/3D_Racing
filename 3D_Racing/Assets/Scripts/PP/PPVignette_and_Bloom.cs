using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPVignette_and_Bloom : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] [Range(0.0f, 1.0f)] private float m_baseVolume;

    [SerializeField] [Range(0.0f, 20.0f)] private float m_baseVolumeBloom;

    [SerializeField] [Range(0.0f, 1.0f)] private float m_volumeModifier;

    [SerializeField] [Range(0.0f, 20.0f)] private float m_volumeModifierBloom;

    private PostProcessVolume _postProcessingVolume;

    private Vignette _vignette;

    private Bloom _bloom;

    private void Start()
    {
        _postProcessingVolume = GetComponent<PostProcessVolume>();

        _postProcessingVolume.profile.TryGetSettings(out _vignette);

        _postProcessingVolume.profile.TryGetSettings(out _bloom);
    }

    private void Update()
    {
        _vignette.intensity.value = m_baseVolume + m_volumeModifier * m_car.NormalizedLinearVelocity;

        _bloom.intensity.value = m_baseVolumeBloom + m_volumeModifierBloom * m_car.NormalizedLinearVelocity;
    }
}
