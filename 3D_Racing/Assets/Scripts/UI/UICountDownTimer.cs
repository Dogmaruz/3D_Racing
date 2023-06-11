using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker _raceStateTracker;

    [SerializeField] private Text m_text;

    private DG.Tweening.Sequence _sequence;

    public void Construct(RaceStateTracker obj)
    {
        _raceStateTracker = obj;
    }

    private void Start()
    {
        _raceStateTracker.PreparationStarted += OnPreparationStarted;

        _raceStateTracker.Started += OnRaceStarted;

        m_text.enabled = false;
    }

    private void OnDestroy()
    {
        _raceStateTracker.PreparationStarted -= OnPreparationStarted;

        _raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnRaceStarted()
    {
        m_text.enabled = false;

        _sequence.Kill();

        enabled = false;
    }


    private void OnPreparationStarted()
    {
        m_text.enabled = true;

        enabled = true;

        _sequence = DOTween.Sequence()
            .Append(m_text.transform.DOScale(new Vector3(4f, 4f, 4f), 0.45f))
            .Append(m_text.transform.DOScale(new Vector3(1f, 1f, 1f), 0.45f))
            .SetEase(Ease.InOutQuad)
            .SetLink(gameObject)
            .SetLoops(-1);
    }

    private void Update()
    {
        m_text.text = _raceStateTracker.CountDownTimer.Value.ToString("F0");

        if (m_text.text == "0")
        {
            m_text.text = "GO";
        }
    }
}
