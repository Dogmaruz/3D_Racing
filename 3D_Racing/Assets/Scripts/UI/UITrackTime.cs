using UnityEngine;
using UnityEngine.UI;

public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    [SerializeField] private Text m_text;

    private RaceStateTracker _raceStateTracker;

    private RaceTimeTracker _raceTimeTracker;

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    public void Construct(RaceTimeTracker obj)
    {
        _raceTimeTracker = obj;
    }

    private void Start()
    {
        _raceStateTracker.Started += OnRaceStarted;

        _raceStateTracker.Completed += OnRaceCompleted;

        m_text.enabled = false;
    }

    private void Update()
    {
        m_text.text = StringTime.SecondToTimeString(_raceTimeTracker.CurrentTime);
    }

    private void OnRaceStarted()
    {
        m_text.enabled = true;

        enabled = true;
    }

    private void OnRaceCompleted()
    {
        m_text.enabled = false;

        enabled = false;
    }

    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;

        _raceStateTracker.Completed -= OnRaceCompleted;
    }
}
