using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker _raceStateTracker;

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _raceStateTracker.LaunchPreparationStart();
        }
    }
}
