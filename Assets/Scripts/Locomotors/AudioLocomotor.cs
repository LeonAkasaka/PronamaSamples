using IteratorTasks;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class AudioLocomotor : Locomotor
{
    [SerializeField]
    private AudioSource _audioMove = null;
    public AudioSource AudioMove { get { return _audioMove; } }

    [SerializeField]
    private AudioSource _audioRotate = null;
    public AudioSource AudioRotate { get { return _audioRotate; } }

    [SerializeField]
    private AudioSource _audioJump = null;
    public AudioSource AudioJump { get { return _audioJump; } }

    [SerializeField]
    private AudioSource _audioRest = null;
    public AudioSource AudioRest { get { return _audioRest; } }

    public IEnumerable<AudioSource> AudioSources
    {
        get
        {
            if (AudioMove != null) { yield return AudioMove; }
            if (AudioMove != null) { yield return AudioRotate; }
            if (AudioMove != null) { yield return AudioJump; }
            if (AudioMove != null) { yield return AudioRest; }
        }
    }

    public IEnumerable<AudioSource> PlayingAudioSources { get { return AudioSources.Where(x => x.isPlaying); } }

    public override void OnMove(float velocity)
    {
        if (AudioMove != null && !AudioMove.isPlaying) { StopAll(); AudioMove.Play(); }
    }

    public override void OnRotate(float velocity)
    {
        if (AudioRotate != null && !AudioRotate.isPlaying) { StopAll(); AudioRotate.Play(); }
    }

    public override IteratorTasks.Task OnJump(float force)
    {
        if (AudioJump != null && !AudioJump.isPlaying) { StopAll(); AudioJump.Play(); }
        return Task.CompletedTask; // TODO: 再生終わるの待つ？
    }

    public override IteratorTasks.Task OnRest()
    {
        if (AudioRest != null && !AudioRest.isPlaying) { StopAll(); AudioRest.Play(); }
        return Task.CompletedTask; // TODO: 再生終わるの待つ？
    }

    public void StopAll()
    {
        foreach (var audioSource in PlayingAudioSources)
        {
            audioSource.Stop();
        }
    }
}