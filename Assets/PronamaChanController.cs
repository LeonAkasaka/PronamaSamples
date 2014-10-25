using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PronamaChanController : MonoBehaviour
{
    private UnityChanLocomotions _motion;

    [SerializeField]
    private float forwardSpeed = 7.0f;

    [SerializeField]
    private float backwardSpeed = 2.0f;

    [SerializeField]
    private float rotateSpeed = 2.0f;

    [SerializeField]
    private float jumpPower = 3.0f; 

	void Start ()
    {
        var animetor = GetComponent<Animator>();
        _motion = new UnityChanLocomotions(animetor);
	}

	void Update ()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Move(v);
        Rotate(h);

        _motion.Speed = v;
        _motion.Direction = h;

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(_motion.Jump());
        }
	}

    private void Move(float v)
    {
        var velocity = new Vector3(0, 0, v);
        velocity = transform.TransformDirection(velocity);
        velocity *=
            v > 0.1 ? forwardSpeed :
                v < -0.1 ? backwardSpeed :
                    1;
        transform.localPosition += velocity * Time.fixedDeltaTime;
    }

    private void Rotate(float h)
    {
        transform.Rotate(0, h * rotateSpeed, 0);
    }
}
