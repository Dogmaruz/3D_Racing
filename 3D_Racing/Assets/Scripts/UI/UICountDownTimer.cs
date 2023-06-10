using System;
using UnityEngine;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker m_raceStateTracker;

    [SerializeField] private Text m_text;

    public void Construct(RaceStateTracker obj)
    {
        m_raceStateTracker = obj;
    }

    private void Start()
    {
        m_raceStateTracker.PreparationStarted += OnPreparationStarted;

        m_raceStateTracker.Started += OnRaceStarted;

        m_text.enabled = false;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.PreparationStarted -= OnPreparationStarted;

        m_raceStateTracker.Started -= OnRaceStarted;
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
        m_text.text = m_raceStateTracker.CountDownTimer.Value.ToString("F0");

        if (m_text.text == "0")
        {
            m_text.text = "GO";
        }
    }
}
