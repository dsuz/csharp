using UnityEngine;

/// <summary>
/// 弾を発射するコンポーネント。弾のオブジェクトにアタッチして使う。
/// オブジェクトが生成されたら前（Z軸の正方向）に直進する
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    /// <summary>弾が飛ぶ速さ</summary>
    [SerializeField] float _speed = 4f;
    /// <summary>弾が回転する速さ</summary>
    [SerializeField] float _rotateSpeed = 5f;
    /// <summary>弾の生存期間（単位: 秒）</summary>
    [SerializeField] float _lifetime = 1f;
    Rigidbody _rb = null;

    void Start()
    {
        // 弾を飛ばし、生存期間を設定する
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = this.transform.forward * _speed;
        Destroy(this.gameObject, _lifetime);
    }

    void FixedUpdate()
    {
        // 弾を回転させる
        this.transform.Rotate(Vector3.up, _rotateSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        // 敵に当たったら消える
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
