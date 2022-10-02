using UnityEngine;

/// <summary>
/// 敵を制御する。
/// 敵はプレイヤーに近づいてくる。
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float _speed = 2f;
    /// <summary>倒したら得られる得点</summary>
    [SerializeField] int _score = 100;
    GameObject _player = default;
    Rigidbody2D _rb = default;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_player)
        {
            Vector2 dir = _player.transform.position - this.transform.position;
            // パーリンノイズを使って移動速度を揺らしている
            _rb.velocity = dir.normalized * Mathf.PerlinNoise(this.transform.position.x, this.transform.position.y) * _speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 弾が当たったら得点を追加して消す
        if (collision.gameObject.tag == "Bullet")
        {
            SingletonSystem.Instance.AddScore(_score);
            Destroy(this.gameObject);
        }
    }
}
