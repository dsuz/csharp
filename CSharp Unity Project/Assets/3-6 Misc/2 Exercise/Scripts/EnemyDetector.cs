using UnityEngine;

/// <summary>
/// 敵を検出するコンポーネント。
/// Target Range より近くにいる敵（タグに Enemy が設定されたオブジェクト）のうち、最も近くにいる敵をロックオンする。
/// ロックオンしたオブジェクトは Target プロパティで取得できる。
/// </summary>
public class EnemyDetector : MonoBehaviour
{
    /// <summary>索敵範囲</summary>
    [SerializeField] float _targetRange = 4f;
    /// <summary>敵の検出を行う間隔（単位: 秒）</summary>
    [SerializeField] float _detectInterval = 1f;
    /// <summary>ロックオンしているオブジェクト</summary>
    GameObject _target = null;
    float _timer;

    /// <summary>
    /// ロックオンしている敵を取得する
    /// </summary>
    public GameObject Target
    {
        get { return _target; }
    }

    void Update()
    {
        _timer += Time.deltaTime;

        // 一定間隔で検出を行う
        if (_timer > _detectInterval)
        {
            _timer = 0;

            // シーン内の敵を全て取得する
            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach (var enemy in enemyArray)
            {
                // 索敵範囲外の敵は処理しない
                float distance = Vector3.Distance(this.transform.position, enemy.transform.position);

                if (distance < _targetRange)
                {
                    // ロックオンしている敵がいない場合は、enemy をロックオンする。現在の target より enemy が近くに居る場合は、enemy をロックオンする。
                    if (_target == null || distance < Vector3.Distance(this.transform.position, _target.transform.position))
                    {
                        _target = enemy;
                    }
                }
            }
        }

        // ロックオンしているターゲットが索敵範囲外に出たらロックオンをやめる
        if (_target)
        {
            if (_targetRange < Vector3.Distance(this.transform.position, _target.transform.position))
            {
                _target = null;
            }
            else
            {
                // ロックオンしている敵まで線を引く
                Debug.DrawLine(this.gameObject.transform.position, _target.transform.position, Color.red);
            }
        }
    }
}
