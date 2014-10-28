using IteratorTasks;
using System.Collections;
using UnityEngine;

/// <summary>
/// 指定のゲームオブジェクトから離れるように運動する LocomotionController。
/// </summary>
public class AwayFromLocomotionController : LocomotionController
{
    [SerializeField]
    private GameObject _from = null;
    public GameObject From { get { return _from; } }

    [SerializeField]
    private float _maxDistance = 10;
    public float MaxDistance { get { return _maxDistance; } }

    protected override void Start()
    {
        base.Start();
        Task.Run(IterateMotionUpdate);
    }

    private IEnumerator IterateMotionUpdate()
    {
        yield return null;

        while(true)
        {
            if (From == null)
            {
                yield return null;
                continue;
            }

            //TODO: ↓本当は Locomotors.Rotate() で回転させるべき。
            var v = transform.position - From.transform.position; //From ゲームオブジェクトと逆方向のベクトル
            v.y = 0; //高さは無視する
            transform.rotation = Quaternion.LookRotation(v.normalized);

            var distance = Vector3.Distance(transform.position, From.transform.position);
            Locomotors.Move(distance < MaxDistance ? 1.0F : 0.0F);

            yield return null;
        }
    }
}
