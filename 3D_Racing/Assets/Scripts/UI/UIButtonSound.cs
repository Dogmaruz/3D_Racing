using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip m_select;

    [SerializeField] private AudioClip m_click;

    private AudioSource _audioSource;

    private UIButton[] _buttons;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _buttons = GetComponentsInChildren<UIButton>(true);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].PointerEnter += OnPointerEnter;

            _buttons[i].PointerClick += OnPointerClick;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].PointerEnter -= OnPointerEnter;

            _buttons[i].PointerClick -= OnPointerClick;
        }
    }

    private void OnPointerEnter(UIButton button)
    {
        _audioSource.PlayOneShot(m_select);
    }

    private void OnPointerClick(UIButton button)
    {
        _audioSource.PlayOneShot(m_click);
    }
}
