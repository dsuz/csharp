using UnityEngine;

/// <summary>
/// Exercise でのタンクを動かすコンポーネント
/// </summary>
public class TankController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float _moveSpeed = 1f;
    /// <summary>回転速度</summary>
    [SerializeField] float _rotateSpeed = 1f;
    /// <summary>爆発エフェクトのオブジェクト</summary>
    [SerializeField] ExplosionController _explosionObject = default;
    /// <summary>銃口</summary>
    [SerializeField] Transform _muzzle = null;
    /// <summary>射程距離</summary>
    [SerializeField] float _maxFireDistance = 100f;
    Rigidbody _rb = default;
    LineRenderer _line = default;
    /// <summary>Ray が当たった座標</summary>
    Vector3 _rayCastHitPosition;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // クリックしたら爆発する
        if (Input.GetButtonDown("Fire1"))
        {
            Fire1();
        }

        // レイキャストして「レーザーポインターがどこに当たっているか」を調べる
        Ray ray = new Ray(_muzzle.position, this.transform.forward);   // muzzle から正面に ray を飛ばす
        RaycastHit hit;

        // m_rayCastHitPosition の初期値は「muzzle から前方に射程距離だけ伸ばした座標」とする
        _rayCastHitPosition = _muzzle.position + this.transform.forward * _maxFireDistance;
        // 課題: 以下で Physics.Raycast() を使って Ray が衝突する座標を取得し、レーザーが障害物に衝突した時はそこでレーザーが止まるように修正せよ

        // Line Renderer を使ってレーザーを描く
        _line.SetPosition(0, _muzzle.position);
        _line.SetPosition(1, _rayCastHitPosition);

        // 以下、上下左右でタンクを動かす
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0)
        {
            this.transform.Rotate(Vector3.up, h * _rotateSpeed * Time.deltaTime * v >= 0 ? 1 : -1);
        }

        if (v != 0)
        {
            _rb.velocity = v * this.transform.forward * _moveSpeed;
        }
    }

    void Fire1()
    {
        // 課題: 以下のコードでは Muzzle の場所で爆発するが、「レーザーが障害物に衝突した位置」で爆発するように修正せよ
        _explosionObject.Explode(_muzzle.position);
    }
}
