using System;
using UnityEngine;

public class Respawner : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
{
    [SerializeField] private float m_respawnHeight;

    private RaceStateTracker _raceStateTracker;

    private Car _car;

    private CarInputControl _carInputControl;

    private TrackPoint _respawnTrackPoint;

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    public void Construct(Car obj)
    {
        _car = obj;
    }

    public void Construct(CarInputControl obj)
    {
        _carInputControl = obj;
    }

    private void Start()
    {
        _raceStateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    private void OnTrackPointPassed(TrackPoint point)
    {
        _respawnTrackPoint = point;
    }

    private void OnDestroy()
    {
        _raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
    }

    public void Respawn()
    {
        if (_respawnTrackPoint == null) return;

        if (_raceStateTracker.State != RaceState.Race) return;

        _car.Respawn(_respawnTrackPoint.transform.position + _respawnTrackPoint.transform.up * m_respawnHeight, _respawnTrackPoint.transform.rotation);

        _carInputControl.Reset();
    }
}
