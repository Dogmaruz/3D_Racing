using System;
using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular,
    Sprint
}

public class TrackPointCircuit : MonoBehaviour
{
    public event UnityAction<TrackPoint> TrackPointTriggered;
    public event UnityAction<int> LapCompleted;

    [SerializeField] private TrackType m_type;
    public TrackType Type => m_type;

    private TrackPoint[] m_points;

    private int _lapsCompleted = -1;


    private void Start()
    {
        BuildCircuit();

        foreach (var point in m_points)
        {
            point.Triggered += OnTrackPointTriggered;
        }

        m_points[0].AssignAsTarget();
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.IsTarget == false) return;

        trackPoint.Passed();

        trackPoint.Next?.AssignAsTarget();

        TrackPointTriggered?.Invoke(trackPoint);

        if (trackPoint.IsLast)
        {
            _lapsCompleted++;

            if (m_type == TrackType.Sprint)
            {
                LapCompleted?.Invoke(_lapsCompleted);
            }

            if (m_type == TrackType.Circular)
            {
                if (_lapsCompleted > 0)
                {
                    LapCompleted?.Invoke(_lapsCompleted);
                }
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var point in m_points)
        {
            point.Triggered -= OnTrackPointTriggered;
        }
    }

    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit()
    {
        m_points = TrackCircuitBuilder.Build(transform, m_type);
    }
}
