using UnityEngine;
using System.Collections;
using IteratorTasks;

public class RandomWalkLocomiotonController : LocomotionController
{
    [SerializeField]
    private float _distance = 1;
    /// <summary>
    /// 開始位置から、ランダムに定める移動先までの最大距離。
    /// </summary>
    public float Distance { get { return _distance; } }

    [SerializeField]
    private float _interval = 0;
    /// <summary>
    /// 目的地に到着後、次の移動を開始するまでの時間。
    /// </summary>
    public float Interval { get { return _interval; } }

    [SerializeField]
    private bool _isRestable = true;
    /// <summary>
    /// true なら目的地に到着したときに休憩モーションを挟む。
    /// </summary>
    public bool IsRestable { get { return _isRestable; } }

    [SerializeField]
    private float _contactDistance = 1.0F;
    /// <summary>
    /// 現在地との差で目的地に到着したと判定する閾値。
    /// </summary>
    /// <remarks>
    /// とりあえず固定しているが SerializeField でパラメータ化を検討。
    /// </remarks>
    private float ContactDistance { get { return _contactDistance; } }

    [SerializeField]
    private float _slowDownDistance = 3.0F;
    /// <summary>
    /// 目的地までの移動中、減速を開始する距離。
    /// </summary>
    /// <remarks>
    /// とりあえず固定しているが SerializeField でパラメータ化を検討。
    /// </remarks>
    private float SlowDownDistance { get { return _slowDownDistance; } }

    [SerializeField]
    private float _straightMovingSpeed = 1.0F;
    /// <summary>
    /// 目的地にまっすぐ向かっているときの移動基準速度。
    /// </summary>
    private float StraightMovingSpeed { get { return _straightMovingSpeed; } }

    [SerializeField]
    private float _rotationMovingSpeed = 0.4F;
    /// <summary>
    /// 目的地に向かって回転しているときの移動基準速度。
    /// </summary>
    private float RotationMovingSpeed { get { return _rotationMovingSpeed; } }

    public Vector3 StartPosition { get; private set; }

    protected override void Start()
    {
        base.Start();
        StartPosition = transform.position;
        Task.Run(IterateMotionUpdate);
    }

    private IEnumerator IterateMotionUpdate()
    {
        yield return null;

        while(true)
        {
            var targetPosition = GetRandomPosition();
            yield return IterateMoveToTarget(targetPosition);

            if (IsRestable) { yield return IterateRest(); }

            yield return Task.Delay((int)Mathf.Floor(Interval * 1000));
        }
    }

    private IEnumerator IterateMoveToTarget(Vector3 targetPosition)
    {
        var isOpposite = false;
        System.Func<Vector3> next = () => transform.position - targetPosition;
        for (var t = next(); t.magnitude > ContactDistance; t = next())
        {
            var targetDirection = (targetPosition - transform.position);
            var angle = Vector3.Angle(transform.forward, targetDirection);

            //TODO: ↓回転移動させるかどうかの閾値、とりあえず固定。SerializeField でパラメータ化するか検討。
            if (angle < 5) { isOpposite = true; }
            if (isOpposite && angle > 20) { isOpposite = false; }

            if (isOpposite) { MoveStraight(t.magnitude); }
            else { MoveRotation(targetDirection); }

            yield return null;
        }

        Locomotors.Move(0F);
    }

    private IEnumerator IterateRest()
    {
        while (!Locomotors.CanRest())
        {
            yield return null;
        }
        yield return Locomotors.Rest();
    }

    private Vector3 GetRandomPosition()
    {
        var x = (-1 + (Random.value * 2)) * Distance;
        var z = (-1 + (Random.value * 2)) * Distance;
        return StartPosition + new Vector3(x, 0, z);
    }

    private void MoveStraight(float length)
    {
        var d = Mathf.Max(0, SlowDownDistance - length);

        Locomotors.Rotate(0.0F);
        Locomotors.Move(StraightMovingSpeed * (1 - (d / SlowDownDistance)));
    }

    private void MoveRotation(Vector3 targetDirection)
    {
        //TODO: ↓ ILocomotor を指定のベクトルに向かせる処理は一般化した方がよさそう。
        var cross = Vector3.Cross(transform.forward, targetDirection);
        var r = cross.y > 0 ? 1.0F : -1.0F;

        Locomotors.Rotate(r);
        Locomotors.Move(RotationMovingSpeed);
    }
}
