using UnityEngine;

public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker _raceStateTracker;

    private float _currentTime;
    public float CurrentTime => _currentTime;

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    private void Start()
    {
        _raceStateTracker.Started += OnRaceStarted;

        _raceStateTracker.Completed += OnRaceCompleted;

        enabled = false;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
    }

    private void OnRaceStarted()
    {
        enabled = true;

        _currentTime = 0;
    }

    private void OnRaceCompleted()
    {
        enabled = false;
    }

    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;

        _raceStateTracker.Completed -= OnRaceCompleted;
    }
}
