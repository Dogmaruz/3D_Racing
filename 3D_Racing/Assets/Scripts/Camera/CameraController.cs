using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Car m_car;

    [SerializeField] private Camera m_camera;

    [SerializeField] private CameraFollow m_follow;

    [SerializeField] private CameraShaker m_shaker;

    [SerializeField] private CameraFovCorrector m_fovCorrector;

    [SerializeField] private RaceStateTracker m_raceStateTracker;

    [SerializeField] private CameraPathFollower m_pathFollower;

    private void Awake()
    {
        m_follow.SetProperties(m_car, m_camera);

        m_shaker.SetProperties(m_car, m_camera);

        m_fovCorrector.SetProperties(m_car, m_camera);
    }

    private void Start()
    {
        m_raceStateTracker.PreparationStarted += OnPreparationStarted;

        m_raceStateTracker.Completed += OnCompleted;

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

        m_pathFollower.SetLookTarget(m_car.transform);

        m_follow.enabled = false;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.PreparationStarted -= OnPreparationStarted;

        m_raceStateTracker.Completed -= OnCompleted;
    }
}
