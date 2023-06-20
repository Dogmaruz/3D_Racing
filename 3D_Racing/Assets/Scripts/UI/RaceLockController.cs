using System;
using System.Collections.Generic;
using UnityEngine;

public class RaceLockController : MonoBehaviour
{
    [SerializeField] private Transform m_parent;

    private List<UIRaceButton> _allRaces;

    private List<RaceState> _raceState;

    public const string filename = "race.dat";

    [Serializable]
    public class RaceState
    {
        public bool IsCompleted;

        public string Name;
    }

    private void Awake()
    {
        _raceState = new List<RaceState>();

        _allRaces = new List<UIRaceButton>();

        for (int i = 0; i < m_parent.childCount; i++)
        {
            _raceState.Add(new RaceState());

            _raceState[i].IsCompleted = m_parent.GetChild(i).gameObject.GetComponent<UIRaceButton>().GetRaceState();

            _raceState[i].Name = m_parent.GetChild(i).gameObject.GetComponent<UIRaceButton>().RaceInfo.SceneName;

            _allRaces.Add(m_parent.GetChild(i).gameObject.GetComponent<UIRaceButton>());

        }

        if (FileHandler.HasFile(filename))
        {
            Saver<List<RaceState>>.TryLoad(filename, ref _raceState);

            for (int i = 0; i < _raceState.Count; i++)
            {
                _allRaces[i].SetRaceState(_raceState[i].IsCompleted);
            }
        }

        if (FileHandler.HasFile(filename) == false)
        {
            Saver<List<RaceState>>.Save(filename, _raceState);
        }

        TryUnlockRace(_raceState);
    }

    private void TryUnlockRace(List<RaceState> raceState)
    {
        for (int i = 0; i < raceState.Count; i++)
        {
            if (i == 0)
            {
                _allRaces[i].SetLockImageVisable(false);

                continue;
            }

            if (_allRaces[i - 1].GetRaceState() == true)
            {
                _allRaces[i].SetLockImageVisable(false);
            }
            else
            {
                _allRaces[i].SetLockImageVisable(true);
            }
        }
    }
}
