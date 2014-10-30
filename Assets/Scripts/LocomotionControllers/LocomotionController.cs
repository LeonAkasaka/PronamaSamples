using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Locomotor を操作するスクリプトの基底クラス。
/// </summary>
public abstract class LocomotionController : MonoBehaviour
{
    private Locomotor[] _locomotors;

    /// <summary>
    /// ゲームオブジェクトにアタッチされている全ての ILocomotor オブジェクトを取得する。
    /// </summary>
    public IEnumerable<ILocomotor> Locomotors { get { return _locomotors; } }

    protected virtual void Start()
    {
        _locomotors = GetComponents<Locomotor>();
	}
}
