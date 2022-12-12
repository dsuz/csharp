using UnityEngine;

/// <summary>
/// 線分AB, PQ の交差判定をするコンポーネント
/// AB と PQ が交差している時は、交点を表示する
/// </summary>
public class DetectCrossing : MonoBehaviour
{
    /// <summary>交点のオブジェクト</summary>
    [SerializeField] Transform _crossingPoint;
    /// <summary>点Aのオブジェクト</summary>
    [SerializeField] Transform _a;
    /// <summary>点Bのオブジェクト</summary>
    [SerializeField] Transform _b;
    /// <summary>点Pのオブジェクト</summary>
    [SerializeField] Transform _p;
    /// <summary>点Qのオブジェクト</summary>
    [SerializeField] Transform _q;
    /// <summary>線分ABを描くための Line Renderer</summary>
    [SerializeField] LineRenderer _lineForAB;
    /// <summary>線分PQを描くための Line Renderer</summary>
    [SerializeField] LineRenderer _lineForPQ;
    /// <summary>線分AB, PQが交差している時は true</summary>
    bool _isCrossing = false;

    void Update()
    {
        // 線を引く
        _lineForAB.SetPosition(0, _a.position);
        _lineForAB.SetPosition(1, _b.position);
        _lineForPQ.SetPosition(0, _p.position);
        _lineForPQ.SetPosition(1, _q.position);

        // 交差判定・交点計算の準備
        Vector3 pq = _q.position - _p.position; // 交差判定に使う
        Vector3 pa = _a.position - _p.position; // 交差判定に使う
        Vector3 pb = _b.position - _p.position; // 交差判定に使う
        Vector3 ab = _b.position - _a.position; // 交差判定・交点の計算に使う
        Vector3 ap = _p.position - _a.position; // 交差判定・交点の計算に使う
        Vector3 aq = _q.position - _a.position; // 交差判定・交点の計算に使う
        Vector3 bq = _q.position - _b.position; // 交点の計算に使う
        Vector3 bp = _p.position - _b.position; // 交点の計算に使う

        // 交差判定する（交差判定には https://nekojara.city/unity-line-segment-cross で説明されている「PからみてQがABの間にある...」という方法を使っている）
        if (Vector3.Cross(pq, pb).z * Vector3.Cross(pq, pa).z < 0 && Vector3.Cross(ab, ap).z * Vector3.Cross(ab, aq).z < 0)
        {
            _isCrossing = true;
        }   // 交差している
        else
        {
            _isCrossing = false;
        }   // 交差していない

        _crossingPoint.gameObject.SetActive(_isCrossing);   // 交差していれば交点を表示する、していなければ消す

        // 交差していたら交点を計算する
        if (_isCrossing)
        {
            // 交点の計算を計算し、オブジェクトを交点に移動する（交点の計算には「ベクトルAPとベクトルAQの外積ベクトルの長さはAPとAQから成る平行四辺形の面積に等しい」という法則を使っている）
            Vector3 apXaq = Vector3.Cross(ap, aq);
            Vector3 bpXbq = Vector3.Cross(bp, bq);
            _crossingPoint.position = _a.position + ab * apXaq.magnitude / (apXaq.magnitude + bpXbq.magnitude);
        }
    }
}
