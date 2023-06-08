using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public event UnityAction Finished;

    [SerializeField] private float  m_time;

    private float _value;

    public float Value => _value;

    private void Start()
    {
        _value = m_time;
    }

    private void Update()
    {
        _value -= Time.deltaTime;

        if (_value <= 0 )
        {
            enabled = false;

            Finished?.Invoke();
        }
    }
}
