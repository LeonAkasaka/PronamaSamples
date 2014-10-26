using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの入力でゲームオブジェクトを動かす LocomotionContoller。
/// </summary>
public class InputLocoimotionController : LocomotionController
{
    private void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Locomotors.Move(v);
        Locomotors.Rotate(h);

        if (Input.GetButtonDown("Jump"))
        {
            Locomotors.Jump(1.0F);
        }
    }
}
