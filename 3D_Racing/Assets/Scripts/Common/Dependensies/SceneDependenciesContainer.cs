using UnityEngine;

public class SceneDependenciesContainer : Dependency
{
    [SerializeField] private RaceStateTracker m_raceSatteTracker;

    [SerializeField] private RaceTimeTracker m_raceTimeTracker;

    [SerializeField] private RaceResultTime m_raceResultTime;

    [SerializeField] private CarInputControl m_carInputControl;

    [SerializeField] private TrackPointCircuit m_trackPointCircuit;

    [SerializeField] private Car m_car;

    [SerializeField] private CameraController m_cameraController;

    protected override void BindFoundMonoBehaviour(MonoBehaviour mono)
    {
        Bind<RaceStateTracker>(mono, m_raceSatteTracker);

        Bind<RaceTimeTracker>(mono, m_raceTimeTracker);

        Bind<RaceResultTime>(mono, m_raceResultTime);

        Bind<CarInputControl>(mono, m_carInputControl);

        Bind<TrackPointCircuit>(mono, m_trackPointCircuit);

        Bind<Car>(mono, m_car);

        Bind<CameraController>(mono, m_cameraController);

        // Старый вариант без обобщения.

        //if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(m_raceSatteTracker);

        //if (mono is IDependency<RaceTimeTracker>) (mono as IDependency<RaceTimeTracker>).Construct(m_raceTimeTracker);

        //if (mono is IDependency<RaceResultTime>) (mono as IDependency<RaceResultTime>).Construct(m_raceResultTime);

        //if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(m_carInputControl);

        //if (mono is IDependency<TrackPointCircuit>) (mono as IDependency<TrackPointCircuit>).Construct(m_trackPointCircuit);

        //if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(m_car);

        //if (mono is IDependency<CameraController>) (mono as IDependency<CameraController>).Construct(m_cameraController);
    }

    private void Awake()
    {
        FindAllObjectToBind();
    }
}
