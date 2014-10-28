using IteratorTasks;

/// <summary>
/// キャラクターの基本的な運動を提供するインターフェイス。
/// 空間座標の移動とアニメーションを実装から切り離すため、このインターフェイス越しに操作する。
/// </summary>
public interface ILocomotor
{
    /// <summary>
    /// ジャンプ可能なら true、そうでなければ false。
    /// </summary>
    bool CanJump { get; }

    /// <summary>
    /// 移動（前進・後退）する
    /// </summary>
    /// <param name="velocity">移動速度。正なら前進、負なら後退。</param>
    void Move(float velocity);

    /// <summary>
    /// 回転する
    /// </summary>
    /// <param name="velocity">回転速度。正なら時計回り、負なら反時計回り。</param>
    void Rotate(float velocity);

    /// <summary>
    /// ジャンプする
    /// </summary>
    /// <param name="velocity">ジャンプの強さ</param>
    Task Jump(float force);
}