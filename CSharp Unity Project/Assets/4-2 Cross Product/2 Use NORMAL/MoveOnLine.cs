using UnityEngine;

/// <summary>
/// Init() の引数として与えられた座標に沿ってオブジェクトを動かすコンポーネント
/// 動く時に、法線 (Normal）方向に直立し続ける
/// この仕様を実現するには必ずしも外積を使う必要はないが、外積を知るための例として外積を使う
/// </summary>
public class MoveOnLine : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float _speed = 0.2f;
    /// <summary>現在の目標地点をシーン上に表示するための空オブジェクト。このオブジェクトの座標に向かって移動する。</summary>
    [SerializeField] Transform _target;
    /// <summary>_target にこの距離まで近づいたら「到達した」とみなす</summary>
    [SerializeField] float _stoppingDistance = 0.01f;
    /// <summary>向きを変える時に滑らかに向きを変えるが、向きを変える速度を指定する</summary>
    [SerializeField] float _rotateSpeed = 1.0f;
    /// <summary>これらの座標方向に順番に移動する</summary>
    Vector3[] _positions;
    /// <summary>現在のターゲットとなっている座標の _positions に対する index</summary>
    int _targetIndex = 0;
    /// <summary>このオブジェクトにとっての現在の「上」方向。この方向に向かって滑らかに回転させる</summary>
    Vector3 _targetUpDirection;
    /// <summary>動いている時は true となる。</summary>
    bool _isWorking = false;

    void Update()
    {
        if (!_isWorking) return;    // 動いていない時は何もしない

        if (Vector3.Distance(this.transform.position, _target.position) < _stoppingDistance)
        {
            if (_targetIndex < _positions.Length - 1)
            {
                _targetIndex++;
                _target.position = _positions[_targetIndex];
                // 接線を計算するする
                Vector3 dir = _target.position - _positions[_targetIndex - 1];
                // 「接線」と「画面手前方向の単位ベクトル」から法線を計算し、それを「上方向のベクトル」とする
                _targetUpDirection = Vector3.Cross(dir.normalized, Vector3.back);
            }   // まだ最終地点ではない場合はターゲットを次の座標に変更する
            else
            {
                _isWorking = false;
            }   // 最終到達点（_positions の最後の座標）に到達した
        }   // ターゲットに到達したかチェックする
        else
        {
            Vector3 dir = _target.position - this.transform.position;   // 進行方向
            this.transform.Translate(_speed * Time.deltaTime * dir.normalized, Space.World);
        }   // まだターゲットに到達していない場合はターゲットに向けて移動する

        if (_targetUpDirection != Vector3.zero)
        {
            this.transform.up = Vector3.Slerp(this.transform.up, _targetUpDirection, Time.deltaTime * _rotateSpeed);
        }   // 滑らかに回転させる
    }

    /// <summary>
    /// 座標の配列を与え、移動処理を開始する
    /// </summary>
    /// <param name="positions">移動する座標の配列</param>
    public void Init(Vector3[] positions)
    {
        _positions = positions;
        this.transform.position = _positions[0];    // 最初の座標に移動する
        // 2番目の座標を最初のターゲットにする
        _targetIndex = 1;
        _target.position = _positions[_targetIndex];
        _isWorking = true;
    }

    /// <summary>
    /// 動きを止める
    /// </summary>
    public void Stop()
    {
        _isWorking = false;
    }
}
