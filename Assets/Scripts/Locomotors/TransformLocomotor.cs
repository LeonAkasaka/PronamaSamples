using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームオブジェクトの Transform を操作する Locomotor の実装。
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TransformLocomotor : Locomotor, ILocomotor
{
    [SerializeField]
    private float _forwardSpeed = 7.0f;
    public float ForwardSpeed { get { return _forwardSpeed; } }

    [SerializeField]
    private float _backwardSpeed = 2.0f;
    public float BackwardSpeed { get { return _backwardSpeed; } }

    [SerializeField]
    private float _rotateSpeed = 2.0f;
    public float RotateSpeed { get { return _rotateSpeed; } }

    [SerializeField]
    private float _jumpPower = 3.0f;
    public float JumpPower { get { return _jumpPower; } }

    public override bool CanJump { get { return true; } } //TODO: 地面と接しているか調べる

    public override void Move(float velocity)
    { 
        var v = new Vector3(0, 0, velocity);
        v = transform.TransformDirection(v);
        v *=
            velocity > 0.1 ? ForwardSpeed :
                velocity < -0.1 ? BackwardSpeed :
                    1;
        transform.localPosition += v * Time.fixedDeltaTime;
    }

    public override void Rotate(float velocity)
    {
        transform.Rotate(0, velocity * RotateSpeed, 0);
    }

    public override void Jump(float force)
    {
        var v = Vector3.up * force * JumpPower;
        rigidbody.AddForce(v, ForceMode.VelocityChange);
    }
}
