using IteratorTasks;
using UnityEngine;

/// <summary>
/// UnityChanLocomotor アニメーションを再生する Locomotor の実装。
/// </summary>
[RequireComponent(typeof(Animator))]
public class UnityChanLocomotor : Locomotor
{
    private UnityChanLocomotions _motion;
    public UnityChanLocomotions Motion { get { return _motion; } }

    public override bool CanJump { get { return Motion.CanJump; } }

    public virtual void Start()
    {
        var animetor = GetComponent<Animator>();
        _motion = new UnityChanLocomotions(animetor);
    }

    public override void OnMove(float velocity)
    {
        Motion.Speed = velocity;
    }

    public override void OnRotate(float velocity)
    {
        Motion.Direction = velocity;
    }

    public override Task OnJump(float force)
    {
        return Motion.Jump();
    }
}