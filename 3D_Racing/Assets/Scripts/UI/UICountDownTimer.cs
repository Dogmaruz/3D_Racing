using System;
using UnityEngine;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker _raceStateTracker;

    [SerializeField] private Text m_text;

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    private void Start()
    {
        _raceStateTracker.PreparationStarted += OnPreparationStarted;

        _raceStateTracker.Started += OnRaceStarted;

        m_text.enabled = false;
    }

    private void OnDestroy()
    {
        _raceStateTracker.PreparationStarted -= OnPreparationStarted;

        _raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnRaceStarted()
    {
        m_text.enabled = false;

        enabled = false;
    }


    private void OnPreparationStarted()
    {
        m_text.enabled = true;

        enabled = true;
    }

    private void Update()
    {
        m_text.text = _raceStateTracker.CountDownTimer.Value.ToString("F0");

        if (m_text.text == "0")
        {
            m_text.text = "GO";
        }
    }
}
