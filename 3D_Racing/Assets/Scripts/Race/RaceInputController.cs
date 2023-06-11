using System;
using UnityEngine;

public class RaceInputController : MonoBehaviour, IDependency<CarInputControl>, IDependency<RaceStateTracker>
{
    private CarInputControl _carInputControl;

    private RaceStateTracker _raceStateTracker;

    public void Construct(CarInputControl obj)
    {
        _carInputControl = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    private void Start()
    {
        _raceStateTracker.Started += OnRaceStarted;

        _raceStateTracker.Completed += OnRaceFinished;

        _carInputControl.enabled = false;
    }

    private void OnRaceStarted()
    {
        _carInputControl.enabled = true;
    }

    private void OnRaceFinished()
    {
        _carInputControl.Stop();

        _carInputControl.enabled = false;
    }

    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;

        _raceStateTracker.Completed -= OnRaceFinished;
    }
}
