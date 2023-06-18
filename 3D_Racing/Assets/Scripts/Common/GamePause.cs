using UnityEngine;
using UnityEngine.Events;

public class GamePause : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChange;

    private bool _isPause;
    public bool IsPause => _isPause;

    public void ChangePauseState()
    {
        if (_isPause == true)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        if (_isPause == true) return;

        Time.timeScale = 0f;

        _isPause = true;

        PauseStateChange?.Invoke(IsPause);

    }

    public void UnPause()
    {
        if (IsPause == false) return;

        Time.timeScale = 1f;

        _isPause = false;

        PauseStateChange?.Invoke(IsPause);
    }
}
