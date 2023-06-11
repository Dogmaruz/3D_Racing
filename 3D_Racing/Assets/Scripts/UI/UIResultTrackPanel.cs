using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIResultTrackPanel : MonoBehaviour, IDependency<RaceResultTime>
{
    [SerializeField] private Text m_bestTime;

    [SerializeField] private Text m_currentTime;

    [SerializeField] private GameObject m_resultTrackPanel;

    private RaceResultTime _raceResultTime;

    private DG.Tweening.Sequence _sequence;

    public void Construct(RaceResultTime obj)
    {
        _raceResultTime = obj;
    }

    private void Start()
    {
        m_resultTrackPanel.SetActive(false);

        _raceResultTime.ResultUpdated += ShowResultPanel;
    }

    private void ShowResultPanel()
    {
        m_resultTrackPanel.SetActive(true);

        _sequence = DOTween.Sequence()
            .Append(m_resultTrackPanel.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.4f))
            .Append(m_resultTrackPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f))
            .SetEase(Ease.InOutQuad)
            .SetLink(gameObject);

        m_bestTime.text = StringTime.SecondToTimeString(_raceResultTime.PlayerRecordTime);

        m_currentTime.text = StringTime.SecondToTimeString(_raceResultTime.CurrentTime);
    }
}
