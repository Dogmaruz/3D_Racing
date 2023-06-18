using System;
using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependency<GamePause>
{
    [SerializeField] private GameObject m_pausePanel;

    private GamePause _gamePause;

    public void Construct(GamePause obj)
    {
        _gamePause = obj;
    }

    private void Start()
    {
        m_pausePanel?.SetActive(false);

        _gamePause.PauseStateChange += OnPauseStateChange;
    }

    private void OnDestroy()
    {
        _gamePause.PauseStateChange -= OnPauseStateChange;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            _gamePause.ChangePauseState();
        }
    }

    private void OnPauseStateChange(bool isPause)
    {
        m_pausePanel?.SetActive(isPause);
    }

    public void UnPause()
    {
        _gamePause.UnPause();
    }
}
