using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

/// <summary>
/// ゲームを管理する。適当なオブジェクトにアタッチし、各種設定をすれば動作する。
/// </summary>
public class GameManager : MonoBehaviour
{
    [Tooltip("Windows のマウスカーソルをゲーム中に消すかどうかの設定")]
    [SerializeField] bool _hideSystemMouseCursor = false;
    [Tooltip("敵オブジェクトがいるレイヤー")]
    [SerializeField] LayerMask _enemyLayer;
    [Tooltip("照準の Image (UI)")]
    [SerializeField] Image _crosshairImage = null;
    [Tooltip("照準に敵が入っていない時の色")]
    [SerializeField] Color _colorNormal = Color.white;
    [Tooltip("照準に敵が入っている時の色")]
    [SerializeField] Color _colorFocus = Color.red;
    [Tooltip("銃のオブジェクト")]
    [SerializeField] GameObject _gunObject = null;
    [Tooltip("銃の操作のために Ray を飛ばす距離")]
    [SerializeField] float _rangeDistance = 100f;
    [Tooltip("スコアを表示するための Text (UI)")]
    [SerializeField] Text _scoreText = null;
    [Tooltip("ライフの初期値")]
    [SerializeField] int _initialLife = 500;
    [Tooltip("ライフを表示するための Text (UI)")]
    [SerializeField] Text _lifeText = null;
    [Tooltip("弾を撃った時に呼び出す処理")]
    [SerializeField] UnityEvent _onShoot = null;
    [Tooltip("ゲームスタート時に呼び出す処理")]
    [SerializeField] UnityEvent _onGameStart = null;
    [Tooltip("ゲームオーバー時に呼び出す処理")]
    [SerializeField] UnityEvent _onGameOver = null;
    /// <summary>ライフの現在値</summary>
    int _life;
    /// <summary>ゲームのスコア</summary>
    int _score = 0;
    /// <summary>全ての敵オブジェクトを入れておくための List</summary>
    List<GunEnemyController> _enemies = null;
    /// <summary>現在照準で狙われている敵</summary>
    GunEnemyController _currentTargetEnemy = null;
    /// <summary>ライフを表示するための GameObject</summary>
    GameObject _lifeObject;

    /// <summary>
    /// ゲームを初期化する
    /// </summary>
    void Start()
    {
        if (_hideSystemMouseCursor)
        {
            Cursor.visible = false;
        }

        _onGameStart.Invoke();
        _life = _initialLife;
        _enemies = GameObject.FindObjectsOfType<GunEnemyController>().ToList();
        _lifeObject = GameObject.Find("LifeText");
        _lifeText = _lifeObject.GetComponent<Text>();
        _lifeText.text = string.Format("{0:000}", _life);
    }

    /// <summary>
    /// ゲームをやり直す
    /// </summary>
    public void Restart()
    {
        Debug.Log("Restart");
        _enemies.ForEach(enemy => enemy.gameObject.SetActive(true));
        Start();
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    void Gameover()
    {
        Debug.Log("Gameover");
        _enemies.ForEach(enemy => enemy.gameObject.SetActive(false));
        _onGameOver.Invoke();
    }

    void Update()
    {
        // 照準を処理する
        _crosshairImage.rectTransform.position = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rangeDistance))
        {
            _gunObject.transform.LookAt(hit.point);    // 銃の方向を変えている
        }

        // 敵が照準に入っているか調べる
        bool isEnemyTargeted = Physics.Raycast(ray, out hit, _rangeDistance, _enemyLayer);
        _crosshairImage.color = isEnemyTargeted ? _colorFocus : _colorNormal;    // 三項演算子 ? を使っている
        _currentTargetEnemy = isEnemyTargeted ? hit.collider.gameObject.GetComponent<GunEnemyController>() : null;    // 三項演算子 ? を使っている

        // 左クリック入力時の処理
        if (Input.GetButtonDown("Fire1"))
        {
            _onShoot.Invoke();

            // 敵に当たったら得点を足して表示を更新する
            if (_currentTargetEnemy)
            {
                _currentTargetEnemy.Hit();
            }
        }
    }

    private void OnApplicationQuit()
    {
        Cursor.visible = true;
    }

    /// <summary>
    /// 攻撃を食らった時に呼ぶ
    /// </summary>
    public void Hit()
    {
        // ライフを減らして表示を更新する。
        _life -= 1;
        Debug.Log($"Hit by enemy. Life: {_life}");
        _lifeText.text = string.Format("{0:000}", _life);
        
        if (_life < 1)
        {
            Gameover();
        }
    }
}
