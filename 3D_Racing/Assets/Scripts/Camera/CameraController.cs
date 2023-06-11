using UnityEngine;

public class CameraController : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
{
    [SerializeField] private Camera m_camera;

    [SerializeField] private CameraFollow m_follow;

    [SerializeField] private CameraShaker m_shaker;

    [SerializeField] private CameraFovCorrector m_fovCorrector;

    [SerializeField] private CameraPathFollower m_pathFollower;

    private RaceStateTracker _raceStateTracker;

    private Car _car;

    public void Construct(Car obj)
    {
        _car = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    private void Awake()
    {
        m_follow.SetProperties(_car, m_camera);

        m_shaker.SetProperties(_car, m_camera);

        m_fovCorrector.SetProperties(_car, m_camera);
    }

    private void Start()
    {
        _raceStateTracker.PreparationStarted += OnPreparationStarted;

        _raceStateTracker.Completed += OnCompleted;

        m_follow.enabled = false;

        m_pathFollower.enabled = true;
    }

    private void OnPreparationStarted()
    {
        m_follow.enabled = true;

        m_pathFollower.enabled = false;
    }

    private void OnCompleted()
    {
        m_pathFollower.enabled = true;

        m_pathFollower.StartMoveToNearestPoint();

        m_pathFollower.SetLookTarget(_car.transform);

        m_follow.enabled = false;
    }

    private void OnDestroy()
    {
        _raceStateTracker.PreparationStarted -= OnPreparationStarted;

        _raceStateTracker.Completed -= OnCompleted;
    }
}
