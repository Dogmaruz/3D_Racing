using System;
using UnityEngine;

public class RaceInputController : MonoBehaviour, IDependency<CarInputControl>, IDependency<RaceStateTracker>
{
    private CarInputControl m_carInputControl;

    private RaceStateTracker m_raceStateTracker;

    public void Construct(CarInputControl obj)
    {
        m_carInputControl = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        m_raceStateTracker = obj;
    }

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
