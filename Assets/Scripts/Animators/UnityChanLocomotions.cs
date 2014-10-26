using UnityEngine;
using System.Collections;
using System;

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

    public UnityChanLocomotions(Animator animator)
    {
        Animator = animator;
    }

    public IEnumerator Jump()
    {
        if (CanJump)
        {
            Animator.SetBool(JumpParameter, true);
        }
        return IterateInState(JumpState, () => Animator.SetBool(JumpParameter, false));
    }

    public IEnumerator Rest()
    {
        Animator.SetBool(RestParameter, true);
        return IterateInState(RestState, () => Animator.SetBool(RestParameter, false));
    }

    private IEnumerator IterateInState(int state, Action finished)
    {
        do
        {
            //状態移行に１フレーム待つ必要があるので do 文使っている
            yield return null;
        } while (State == state);
        finished();
    }

}
