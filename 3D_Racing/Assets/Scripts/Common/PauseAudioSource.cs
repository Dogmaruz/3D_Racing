using System;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class PauseAudioSource : MonoBehaviour, IDependency<GamePause>
{
    private AudioSource _audio;

    private GamePause _gamePause;

    public void Construct(GamePause obj)
    {
        _gamePause = obj;
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource> ();

        _gamePause.PauseStateChange += OnPauseChange;
    }

    private void OnDestroy()
    {
        _gamePause.PauseStateChange -= OnPauseChange;
    }

    private void OnPauseChange(bool pause)
    {
        if (pause == false) 
        { 
            _audio.Play();
        }

        if (pause == true)
        {
            _audio.Stop();
        }
    }
}
