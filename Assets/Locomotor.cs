using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Locomotor : MonoBehaviour
{
    private UnityChanLocomotions _motion;
    public UnityChanLocomotions Motion { get { return _motion; } }

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

    public virtual void Start()
    {
        var animetor = GetComponent<Animator>();
        _motion = new UnityChanLocomotions(animetor);
    }

    public virtual void Move(float v)
    { 
        var velocity = new Vector3(0, 0, v);
        velocity = transform.TransformDirection(velocity);
        velocity *=
            v > 0.1 ? ForwardSpeed :
                v < -0.1 ? BackwardSpeed :
                    1;
        transform.localPosition += velocity * Time.fixedDeltaTime;
    }

    public virtual void Rotate(float h)
    {
        transform.Rotate(0, h * RotateSpeed, 0);
    }

    public virtual void Jump()
    {
        if (Motion.CanJump)
        {
            rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
        }
    }

}
