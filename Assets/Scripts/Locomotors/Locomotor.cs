using IteratorTasks;
using UnityEngine;

public abstract class Locomotor : MonoBehaviour, ILocomotor
{
    public virtual bool CanJump { get { return true; } }
    public virtual bool CanRest { get { return true; } }

    public void Move(float velocity)
    {
        if (enabled) OnMove(velocity);
    }
    public void Rotate(float velocity)
    {
        if (enabled) OnRotate(velocity);
    }
    public Task Jump(float force)
    {
        if (enabled) { return OnJump(force); }
        return Task.CompletedTask;
    }
    public Task Rest()
    {
        if (enabled) { return OnRest(); }
        return Task.CompletedTask;
    }

    public virtual void OnMove(float velocity) { }
    public virtual void OnRotate(float velocity) { }
    public virtual Task OnJump(float force)
    {
        return Task.CompletedTask;
    }
    public virtual Task OnRest()
    {
        return Task.CompletedTask;
    }
}