using UnityEngine;

public abstract class Locomotor : MonoBehaviour, ILocomotor
{
    public abstract bool CanJump { get; }

    public abstract void Move(float velocity);
    public abstract void Rotate(float velocity);
    public abstract void Jump(float force);
}