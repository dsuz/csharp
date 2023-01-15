using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 線分 OP と OQ のなす角を求め、表示するコンポーネント
/// </summary>
public class AngleCalculator : MonoBehaviour
{
    [Tooltip("点 O")]
    [SerializeField] Transform _o;
    [Tooltip("点 P")]
    [SerializeField] Transform _p;
    [Tooltip("点 Q")]
    [SerializeField] Transform _q;
    [Tooltip("線分 OP を描くコンポーネント")]
    [SerializeField] LineRenderer _op;
    [Tooltip("線分 OQ を描くコンポーネント")]
    [SerializeField] LineRenderer _oq;
    [Tooltip("内積を使って求めた OP と OQ のなす角を表示する UI")]
    [SerializeField] Text _angleByDot;
    [Tooltip("Vector2.Angle() を使って求めた OP と OQ のなす角を表示する UI")]
    [SerializeField] Text _angleByAngle;

    void Update()
    {
        // 線分 OP, OQ を描く
        DrawLines();
        
        // 2通りの方法でOP と OQ のなす角Θを求める
        #region 内積を使う
        Vector2 op = _p.position - _o.position;
        Vector2 oq = _q.position - _o.position;
        float theta = Mathf.Acos(Vector2.Dot(op, oq) / (op.magnitude * oq.magnitude));
        theta *= Mathf.Rad2Deg; // 弧度法（単位: ラジアン）→度数法（単位: 度）に変換する (Mathf.Rad2Deg = 180 / Mathf.PI)
        _angleByDot.text = theta.ToString("00");  
        #endregion
        #region Unity の組込関数 Vector2.Angle() を使う
        theta = Vector2.Angle(op, oq);
        _angleByAngle.text = theta.ToString("00");  // Vector2.Angle を使った時の戻り値は度数法である
        #endregion
    }

    // OP, OQ を描く
    void DrawLines()
    {
        DrawLine(_p, _op);
        DrawLine(_q, _oq);
    }

    // 線を描く
    void DrawLine(Transform tx, LineRenderer line)
    {
        line.SetPosition(0, _o.position);
        line.SetPosition(1, tx.position);
    }
}
