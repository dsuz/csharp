using UnityEngine;

/// <summary>
/// 外積を計算して表示する
/// </summary>
public class CrossProduct : MonoBehaviour
{
    [SerializeField] LineRenderer[] _lines;

    void Update()
    {
        // 1番目の座標と2番目の座標の外積を求め、3番目の座標をそこに動かす
        _lines[2].transform.position = Vector3.Cross(_lines[0].transform.position, _lines[1].transform.position);

        // 座標まで線を引く
        foreach (var line in _lines)
        {
            line.SetPosition(1, line.transform.position);
        }
    }
}
