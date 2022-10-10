using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 拡張メソッド Vector3.Distance() を使うサンプル
/// </summary>
public class UseExtensionMethod : MonoBehaviour
{
    /// <summary>距離をここに出力する</summary>
    [SerializeField] Text _text = default;
    /// <summary>このオブジェクトとの距離を求める</summary>
    [SerializeField] Transform _destination = default;

    void Update()
    {
        // 距離を求めて Text に表示する
        float distance = this.transform.position.Distance(_destination.position);
        _text.text = distance.ToString("000.00");
    }
}
