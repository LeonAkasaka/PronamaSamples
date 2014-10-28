using UnityEngine;
using System.Collections;
using System;
using IteratorTasks;

/// <summary>
/// こんなクラスを Animator 毎に作りたいけど、ほとんどテンプレ作業。
/// 状態ハッシュとパラメータ名の定数、各パラメータに対するプロパティくらいは自動コード生成できそう。
/// </summary>
public class UnityChanLocomotions
{
    #region 状態ハッシュ定数
    public static readonly int IdleState = Animator.StringToHash("Base Layer.Idle");
    public static readonly int LocomationState = Animator.StringToHash("Base Layer.Locomotion");
    public static readonly int JumpState = Animator.StringToHash("Base Layer.Jump");
    public static readonly int RestState = Animator.StringToHash("Base Layer.Rest");
    public static readonly int WalkBackState = Animator.StringToHash("Base Layer.WalkBack");
    #endregion

    #region パラメータ名定数
    public const string SpeedParameter = "Speed";
    public const string DirectionParameter = "Direction";
    public const string JumpParameter = "Jump";
    public const string RestParameter = "Rest";
    public const string JumpHeightParameter = "JumpHeight";
    public const string GravityParameter = "GravityControl";
    #endregion

    public Animator Animator { get; private set; }

    public int State { get { return Animator.GetCurrentAnimatorStateInfo(0).nameHash; } }

    public float Speed { get { return Animator.GetFloat(SpeedParameter); } set { Animator.SetFloat(SpeedParameter, value); } }

    public float Direction { get { return Animator.GetFloat(DirectionParameter); } set { Animator.SetFloat(DirectionParameter, value); } }

    public bool IsJump { get { return Animator.GetBool(JumpParameter); } }

    public bool IsRest { get { return Animator.GetBool(RestParameter); } }

    public float JumpHeight { get { return Animator.GetFloat(JumpHeightParameter); } set { Animator.SetFloat(JumpHeightParameter, value); } }

    public float GravityControl { get { return Animator.GetFloat(GravityParameter); } set { Animator.SetFloat(GravityParameter, value); } }

    public bool CanJump { get { return State == LocomationState && !Animator.IsInTransition(0); } }

    public bool CanRest { get { return State == IdleState && !Animator.IsInTransition(0); } }

    public UnityChanLocomotions(Animator animator)
    {
        Animator = animator;
    }

    public Task Jump()
    {
        var task = Task.CompletedTask;
        if (CanJump)
        {
            task = StartMotion(JumpParameter, JumpState);
        }
        return task;
    }

    public Task Rest()
    {
        var task = Task.CompletedTask;
        if (CanRest)
        {
            task = StartMotion(RestParameter, RestState);
        }
        return task;
    }

    private Task StartMotion(string parameter, int state)
    {
        Animator.SetBool(parameter, true);
        return Task.Run(IterateNotInState(state)) //指定の状態に移行するまで
            .ContinueWithTask(t => Task.Run(IterateInState(state))) //指定の状態が終了するまで
                    .ContinueWith(t => Animator.SetBool(parameter, false));
    }

    private IEnumerator IterateNotInState(int state)
    {
        while (State != state) { yield return null; }
    }
    private IEnumerator IterateInState(int state)
    {
        while (State == state) { yield return null; }
    }

}
