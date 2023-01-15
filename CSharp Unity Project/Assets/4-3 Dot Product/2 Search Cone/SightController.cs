using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コーン状の視野を提供するコンポーネント
/// キャラクターのオブジェクトまたはキャラクターの頭部に追加して使う
/// </summary>
public class SightController : MonoBehaviour
{
    [Tooltip("視点方向を示すオブジェクト")]
    [SerializeField] Transform _lookAtTarget;
    [Tooltip("視野角")]
    [SerializeField, Range(0, 180)] float _sightAngle;
    [Tooltip("見える距離")]
    [SerializeField] private float _sightDistance = 5f;
    [Tooltip("視界に入った場合にメッセージを表示する UI")]
    [SerializeField] Text _visibleMessage;
    /// <summary>発見したいオブジェクト（今回の場合はプレイヤー）</summary>
    Transform _target;
    /// <summary>発見フラグ</summary>
    bool _isVisible = true;

    void Update()
    {
        if (_isVisible ^ IsVisible())
        {
            _isVisible = !_isVisible;
            _visibleMessage.enabled = _isVisible;   // 発見したらメッセージを表示する, 見失ったらメッセージを消す
        }   // 論理積（最後のフレームと現フレームで見える/見えないが切り替わった時）
    }

    bool IsVisible()
    {
        // 発見したいオブジェクトがない場合はプレイヤーを探して割り当てる
        if (!_target)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            
            if (player)
                _target = player.transform;
        }

        Vector3 look = _lookAtTarget.position - this.transform.position;    // 視点方向ベクトル
        Vector3 target = _target.position - this.transform.position;    // ターゲットへのベクトル
        float cosHalfSight = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);    // 視野角（の半分）の余弦
        float cosTarget = Vector3.Dot(look, target) / (look.magnitude * target.magnitude);  // ターゲットへの角度の余弦
        return cosTarget > cosHalfSight && target.magnitude < _sightDistance;   // ターゲットへの角度が視界の角度より小さく、かつ距離が視界より近いなら見えていると判定して true を返す
        // なぜ？ ⇒ 角度が 0 ~ 180（0 ~ π）の時、Θ > Γ ⇔ cosΘ < cosΓ が成り立つから
    }

    void OnDrawGizmos()
    {
        // 視界の範囲（正面及び左右の端）をギズモとして描く
        Vector3 lookAtDirection = (_lookAtTarget.position - this.transform.position).normalized;    // 正面（正規化）
        Vector3 rightBorder = Quaternion.Euler(0, _sightAngle / 2, 0) * lookAtDirection;    // 右端（正規化）
        Vector3 leftBorder = Quaternion.Euler(0, -1 * _sightAngle / 2, 0) * lookAtDirection;    // 左端（正規化）
        Gizmos.color = Color.cyan;  // 正面は水色で描く
        Gizmos.DrawRay(this.transform.position, lookAtDirection * _sightDistance);
        Gizmos.color = Color.blue;  // 両端は青で描く
        Gizmos.DrawRay(this.transform.position, rightBorder * _sightDistance);
        Gizmos.DrawRay(this.transform.position, leftBorder * _sightDistance);
        // 扇形もしくはコーン状の図形を gizmo あるいはゲーム上に表示したい場合は、
        // Mesh を動的に生成して Gizmos.DrawMesh を使う
    }
}