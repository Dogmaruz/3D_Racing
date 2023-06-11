using UnityEngine;
using UnityEngine.UI;

public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
{
    [SerializeField] private GameObject m_goldRecordObject;

    [SerializeField] private GameObject m_playerRecordObject;

    [SerializeField] private Text m_goldRecordTime;

    [SerializeField] private Text m_playerRecordTime;

    private RaceStateTracker _raceStateTracker;

    private RaceResultTime _raceResultTime;

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    public void Construct(RaceResultTime obj)
    {
        _raceResultTime = obj;
    }

    private void Start()
    {
        _raceStateTracker.Started += OnRaceStarted;

        _raceStateTracker.Completed += OnRaceCompleted;

        m_goldRecordObject.SetActive(false);

        m_playerRecordObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;

        _raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        if (_raceResultTime.PlayerRecordTime > _raceResultTime.GoldTime || _raceResultTime.RecordWasSet == false)
        {
            m_goldRecordObject.SetActive(true);

            m_goldRecordTime.text = StringTime.SecondToTimeString(_raceResultTime.GoldTime);
        }

        if (_raceResultTime.RecordWasSet)
        {
            m_playerRecordObject.SetActive(true);

            m_playerRecordTime.text = StringTime.SecondToTimeString(_raceResultTime.PlayerRecordTime);
        }
    }

    private void OnRaceCompleted()
    {
        m_goldRecordObject.SetActive(false);

        m_playerRecordObject.SetActive(false);
    }
}
