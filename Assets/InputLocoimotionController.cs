using UnityEngine;
using System.Collections;

public class InputLocoimotionController : LocomotionController
{
    private void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Locomotor.Move(v);
        Locomotor.Rotate(h);

        Locomotor.Motion.Speed = v;
        Locomotor.Motion.Direction = h;

        if (Input.GetButtonDown("Jump"))
        {
            Locomotor.Jump();
            StartCoroutine(Locomotor.Motion.Jump());
        }
    }
}
