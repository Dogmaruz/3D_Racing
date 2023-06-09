using UnityEngine;

public class CameraPathFollower : CameraComponent
{
    [SerializeField] private Transform m_path;

    [SerializeField] private Transform m_lookTarget;

    [SerializeField] private float  m_movementSpeed;

    private Vector3[] _points;

    private int _pointIndex;

    private void Start()
    {
        _points = new Vector3[m_path.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = m_path.GetChild(i).position;
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _points[_pointIndex], m_movementSpeed * Time.deltaTime);

        if (transform.position == _points[_pointIndex])
        {
            if (_pointIndex == _points.Length -1)
            {
                _pointIndex = 0;
            }
            else
            {
                _pointIndex++;
            }
        }

        transform.LookAt(m_lookTarget);
    }

    public void SetLookTarget(Transform target)
    {
        m_lookTarget = target;
    }

    public void StartMoveToNearestPoint()
    {
        float minDistance = float.MaxValue;

        for (int i = 0; i < _points.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, _points[i]);

            if (distance < minDistance)
            {
                minDistance = distance;

                _pointIndex = i;
            }
        }
    }
}
