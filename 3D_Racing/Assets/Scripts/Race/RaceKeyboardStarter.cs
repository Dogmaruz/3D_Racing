using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker m_raceStateTracker;

    public void Construct(RaceStateTracker obj)
    {
        m_raceStateTracker = obj;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            m_raceStateTracker.LaunchPreparationStart();
        }
    }
}
