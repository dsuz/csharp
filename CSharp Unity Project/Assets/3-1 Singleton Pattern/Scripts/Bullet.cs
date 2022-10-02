using UnityEngine;

/// <summary>
/// 弾を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    /// <summary>弾が飛ぶ速さ</summary>
    [SerializeField] float _speed = 3f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = this.transform.up * _speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
