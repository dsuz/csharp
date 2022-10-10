using UnityEngine;

/// <summary>
/// 拡張メソッドを定義する
/// </summary>
static class Vector3Extensions
{
    /// <summary>
    /// インスタンスを起点座標とし、destination を終点座標として起点から終点までの距離を求める
    /// </summary>
    /// <param name="origin">起点座標</param>
    /// <param name="destination">終点座標</param>
    /// <returns></returns>
    public static float Distance(this Vector3 origin, Vector3 destination)
    {
        return Vector3.Distance(origin, destination);
    }
}
