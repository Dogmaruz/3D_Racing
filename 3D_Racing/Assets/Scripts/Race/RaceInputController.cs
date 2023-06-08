using System;
using UnityEngine;

public class RaceInputController : MonoBehaviour
{
    [SerializeField] private CarInputControl m_carInputControl;

    [SerializeField] private RaceStateTracker m_raceStateTracker;

    private void Start()
    {
        m_raceStateTracker.Started += OnRaceStarted;

        m_raceStateTracker.Completed += OnRaceFinished;

        m_carInputControl.enabled = false;
    }

    private void OnRaceStarted()
    {
        m_carInputControl.enabled = true;
    }

    private void OnRaceFinished()
    {
        m_carInputControl.Stop();

        m_carInputControl.enabled = false;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.Started -= OnRaceStarted;

        m_raceStateTracker.Completed -= OnRaceFinished;
    }
}
