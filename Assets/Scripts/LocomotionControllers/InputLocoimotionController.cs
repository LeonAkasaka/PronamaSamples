using UnityEngine;
using System.Collections;
using IteratorTasks;

/// <summary>
/// プレイヤーの入力でゲームオブジェクトを動かす LocomotionContoller。
/// </summary>
public class InputLocoimotionController : LocomotionController
{
    protected override void Start()
    {
        base.Start();
        Task.Run(IterateMotionUpdate);
    }

    private IEnumerator IterateMotionUpdate()
    {
        yield return null;

        while (true)
        {
            UpdateNormalMotion();
            if (Input.GetButtonDown("Jump"))
            {
                Locomotors.Jump(1.0F);
            }
            yield return null;
        }
    }

    private void UpdateNormalMotion()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        var v = Input.GetAxis("Vertical");
        Locomotors.Move(v);
    }

    private void Rotate()
    {
        var h = Input.GetAxis("Horizontal");
        Locomotors.Rotate(h);
    }
}
