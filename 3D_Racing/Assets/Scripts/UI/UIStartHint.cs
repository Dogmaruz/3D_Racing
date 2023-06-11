using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class UIStartHint : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject m_startHintText;

    private RaceStateTracker _raceStateTracker;

    private DG.Tweening.Sequence _sequence;

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    private void Start()
    {
        m_startHintText.SetActive(true);

        _sequence = DOTween.Sequence()
            .Append(m_startHintText.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f))
            .Append(m_startHintText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f))
            .SetEase(Ease.InOutQuad)
            .SetLink(gameObject)
            .SetLoops(-1);

        _raceStateTracker.PreparationStarted += OnPreparationStarted;
    }

    private void OnDestroy()
    {
        _sequence.Kill();

        _raceStateTracker.PreparationStarted -= OnPreparationStarted;
    }

    private void OnPreparationStarted()
    {
        _sequence.Kill();

        m_startHintText.SetActive(false);
    }
}
