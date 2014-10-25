using UnityEngine;
using System.Collections;

public class UnityChanLocomotions
{
    public static readonly int IdleState = Animator.StringToHash("Base Layer.Idle");
    public static readonly int LocomationState = Animator.StringToHash("Base Layer.Locomotion");
    public static readonly int JumpState = Animator.StringToHash("Base Layer.Jump");
    public static readonly int RestState = Animator.StringToHash("Base Layer.Rest");
    public static readonly int WalkBackState = Animator.StringToHash("Base Layer.WalkBack");

    public Animator Animator { get; private set; }

    public int State { get { return Animator.GetCurrentAnimatorStateInfo(0).nameHash; } }

    public float Speed { get { return Animator.GetFloat("Speed"); } set { Animator.SetFloat("Speed", value); } }

    public float Direction { get { return Animator.GetFloat("Direction"); } set { Animator.SetFloat("Direction", value); } }

    public bool IsJump { get { return Animator.GetBool("Jump"); } }

    public bool IsRest { get { return Animator.GetBool("Rest"); } }

    public float JumpHeight { get { return Animator.GetFloat("JumpHeight"); } set { Animator.SetFloat("JumpHeight", value); } }

    public float GravityControl { get { return Animator.GetFloat("GravityControl"); } set { Animator.SetFloat("GravityControl", value); } }

    public UnityChanLocomotions(Animator animator)
    {
        Animator = animator;
    }

    public IEnumerator Jump()
    {
        if (State == LocomationState && !Animator.IsInTransition(0))
        {
            Animator.SetBool("Jump", true);
        }

        return JumpTaskIteration();
    }

    private IEnumerator JumpTaskIteration()
    {
        do
        {
            yield return null;
        } while (State == JumpState);
        Animator.SetBool("Jump", false);
    }

    public void Rest()
    {
        Animator.SetBool("Rest", true);
    }
}
