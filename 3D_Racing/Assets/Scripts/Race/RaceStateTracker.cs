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

public class RaceStateTracker : MonoBehaviour
{
    public event UnityAction PreparationStarted;

    public event UnityAction Started;

    public event UnityAction Completed;

    public event UnityAction<TrackPoint> TrackointPassed;

    public event UnityAction<int> LapCompleted;

    [SerializeField] private TrackPointCircuit m_trackPointCircuit;

    [SerializeField] private Timer m_countDownTimer;

    [SerializeField] private int m_lapsToCompleted;

    private RaceState _state;
    public RaceState State => _state;

    private void StartState(RaceState state)
    {
        _state = state;
    }

    private void Start()
    {
        StartState(RaceState.Preparation);

        m_countDownTimer.enabled = false;

        m_countDownTimer.Finished += OnCountDownTimerFinished;

        m_trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;

        m_trackPointCircuit.LapCompleted += OnLapCompleted;
    }

    private void OnDestroy()
    {
        m_countDownTimer.Finished -= OnCountDownTimerFinished;

        m_trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;

        m_trackPointCircuit.LapCompleted -= OnLapCompleted;
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
        if (m_trackPointCircuit.Type == TrackType.Sprint) 
        {
            CompletedRace();
        }

        if (m_trackPointCircuit.Type == TrackType.Circular)
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
