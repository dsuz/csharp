using UnityEngine;

/// <summary>
/// プレイヤーを制御するコンポーネント
/// Fire1 で弾を発射する
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Shooter : MonoBehaviour
{
    /// <summary>移動する力</summary>
    [SerializeField] float _movePower = 5f;
    /// <summary>弾を発射地点</summary>
    [SerializeField] Transform _muzzle = default;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject _bulletPrefab = default;
    Rigidbody2D _rb = default;
    Vector2 _dir = default;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        _rb.AddForce(_dir.normalized * _movePower);
    }

    void LateUpdate()
    {
        if (_rb.velocity != Vector2.zero)
        {
            this.transform.up = _rb.velocity;
        }
    }

    void Fire()
    {
        var bullets = GameObject.FindGameObjectsWithTag("Bullet");

        // 画面内の弾数を制限する
        if (bullets.Length < SingletonSystem.Instance.BulletsInScene)
        {
            Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
        }
    }
}
