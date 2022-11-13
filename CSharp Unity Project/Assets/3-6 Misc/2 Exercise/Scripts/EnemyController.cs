using UnityEngine;

/// <summary>
/// 敵の挙動をコントロールするコンポーネント
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    /// <summary>動く時にかける力</summary>
    [SerializeField] float _movePower = 30f;
    /// <summary>最大速度</summary>
    [SerializeField] float _maxSpeed = 4f;
    /// <summary>消える時に残すエフェクトのプレハブ</summary>
    [SerializeField] GameObject _destroyEffectPrefab = null;
    Rigidbody _rb = null;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // プレイヤーの方に移動させる
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player)
        {
            Vector3 dir = player.transform.position - this.transform.position;
            dir.y = 0;  // 上下方向は無視する
            this.transform.forward = dir;

            if (_rb.velocity.magnitude < _maxSpeed)
            {
                _rb.AddForce(this.transform.forward * _movePower);
            }
        }
        else
        {
            // プレイヤーが居なくなったら消える
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // トリガーに当たったら破棄する
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        // 破棄される時にエフェクトを生成する
        if (_destroyEffectPrefab)
        {
            Instantiate(_destroyEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnApplicationQuit()
    {
        // これをしないと停止時にうるさいのでやっておく
        _destroyEffectPrefab = null;
    }
}
