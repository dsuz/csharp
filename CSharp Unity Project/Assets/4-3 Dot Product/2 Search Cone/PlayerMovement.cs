using UnityEngine;

/// <summary>
/// プレイヤーを動かすコンポーネント
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 2f;
    Rigidbody _rb;
    float _h, _v;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(_h, 0, _v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        _rb.velocity = dir * _moveSpeed;
    }
}
