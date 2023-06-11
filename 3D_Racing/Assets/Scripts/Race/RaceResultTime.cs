using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    public static string SaveMark = "_player_best_time";

    public event UnityAction ResultUpdated;

    [SerializeField] private float m_goldTime;
    public float GoldTime => m_goldTime;

    private float _playerRecordTime;
    public float PlayerRecordTime => _playerRecordTime;

    private float _currentTime;
    public float CurrentTime => _currentTime;

    public bool RecordWasSet => _playerRecordTime != 0;

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

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        _raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        float absoluteRecord = GetAbsoluteRecord();

       if (_raceTimeTracker.CurrentTime < absoluteRecord || _playerRecordTime == 0) 
        { 
            _playerRecordTime = _raceTimeTracker.CurrentTime;

            Save();
        }

       _currentTime = _raceTimeTracker.CurrentTime;

        ResultUpdated?.Invoke();
    }

    public float GetAbsoluteRecord()
    {
        if (_playerRecordTime < m_goldTime && _playerRecordTime != 0)
        {
            return _playerRecordTime;
        }
        else
        {
            return m_goldTime;
        }
    }

    private void Load()
    {
        _playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, _playerRecordTime);
    }

    private void OnDestroy()
    {
        _raceStateTracker.Completed -= OnRaceCompleted;
    }
}
