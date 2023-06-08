using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour
{
    [SerializeField] private RaceStateTracker m_raceStateTracker;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            m_raceStateTracker.LaunchPreparationStart();
        }
    }
}
