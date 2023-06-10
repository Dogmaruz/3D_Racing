using UnityEngine;

public interface IDependency<T>
{
    void Construct(T obj);
}

public class SceneDependencies : MonoBehaviour
{
    [SerializeField] private RaceStateTracker m_raceSatteTracker;

    [SerializeField] private CarInputControl m_carInputControl;

    [SerializeField] private TrackPointCircuit m_trackPointCircuit;

    [SerializeField] private Car m_car;

    [SerializeField] private CameraController m_cameraController;

    private void Bind(MonoBehaviour mono)
    {
        if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(m_raceSatteTracker);

        if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(m_carInputControl);

        if (mono is IDependency<TrackPointCircuit>) (mono as IDependency<TrackPointCircuit>).Construct(m_trackPointCircuit);

        if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(m_car);

        if (mono is IDependency<CameraController>) (mono as IDependency<CameraController>).Construct(m_cameraController);

    }

    private void Awake()
    {
        MonoBehaviour[] MonoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < MonoInScene.Length; i++)
        {
            Bind(MonoInScene[i]);
        }
    }
}
