using UnityEngine;

[RequireComponent (typeof(CameraController))]
public abstract class CameraComponent : MonoBehaviour
{
    protected Car m_car;

    protected Camera m_camera;

    public virtual void SetProperties(Car car, Camera camera)
    {
        m_car = car;

        m_camera = camera;
    }
}
