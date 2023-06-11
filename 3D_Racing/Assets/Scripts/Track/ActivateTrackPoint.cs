using UnityEngine;

public class ActivateTrackPoint : TrackPoint
{
    [SerializeField] private GameObject m_hint;

    private void Start()
    {
        m_hint.SetActive(IsTarget);
    }

    protected override void OnPassed()
    {
        m_hint.SetActive(false);
    }

    protected override void OnAssingnAsTarget()
    {
        m_hint.SetActive(true);
    }
}
