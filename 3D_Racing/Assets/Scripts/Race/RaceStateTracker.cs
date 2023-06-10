using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public enum RaceState
{
    Preparation,
    CountDown,
    Race,
    Passed
}

public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCircuit>
{
    public event UnityAction PreparationStarted;

    public event UnityAction Started;

    public event UnityAction Completed;

    public event UnityAction<TrackPoint> TrackointPassed;

    public event UnityAction<int> LapCompleted;

    [SerializeField] private Timer m_countDownTimer;
    public Timer CountDownTimer => m_countDownTimer;

    [SerializeField] private int m_lapsToCompleted;

    private TrackPointCircuit _trackPointCircuit;

    private RaceState _state;
    public RaceState State => _state;


    public void Construct(TrackPointCircuit obj)
    {
        _trackPointCircuit = obj;
    }

    private void StartState(RaceState state)
    {
        _state = state;
    }

    private void Start()
    {
        StartState(RaceState.Preparation);

        m_countDownTimer.enabled = false;

        m_countDownTimer.Finished += OnCountDownTimerFinished;

        _trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;

        _trackPointCircuit.LapCompleted += OnLapCompleted;
    }

    private void OnDestroy()
    {
        m_countDownTimer.Finished -= OnCountDownTimerFinished;

        _trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;

        _trackPointCircuit.LapCompleted -= OnLapCompleted;
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackointPassed?.Invoke(trackPoint);
    }

    private void OnCountDownTimerFinished()
    {
        StartRace();
    }

    private void OnLapCompleted(int lapAmount)
    {
        if (_trackPointCircuit.Type == TrackType.Sprint)
        {
            CompletedRace();
        }

        if (_trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == m_lapsToCompleted)
            {
                CompletedRace();
            }
            else
            {
                CompletedLap(lapAmount);
            }
        }
    }

    public void LaunchPreparationStart()
    {
        if (_state != RaceState.Preparation) return;

        StartState(RaceState.CountDown);

        m_countDownTimer.enabled = true;

        PreparationStarted?.Invoke();
    }

    public void StartRace()
    {
        if (_state != RaceState.CountDown) return;

        StartState(RaceState.Race);

        Started?.Invoke();
    }

    private void CompletedRace()
    {
        if (_state != RaceState.Race) return;

        StartState(RaceState.Passed);

        Completed?.Invoke();
    }

    public void CompletedLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }
}
