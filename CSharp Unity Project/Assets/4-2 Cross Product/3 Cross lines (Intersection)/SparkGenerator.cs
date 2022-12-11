using UnityEngine;

/// <summary>
/// ランダムな場所にオブジェクトを生成するコンポーネント
/// 最初にいくつかランダムな場所にオブジェクトを生成しておき、時間が経過するにつれて追加で生成する
/// </summary>
public class SparkGenerator : MonoBehaviour
{
    /// <summary>生成するオブジェクトのプレハブ</summary>
    [SerializeField] GameObject _prefab;
    /// <summary>最初に生成するオブジェクトの数</summary>
    [SerializeField] int _initialCount = 50;
    /// <summary>ここで設定した時間ごとに追加でオブジェクトが生成される（秒）</summary>
    [SerializeField] float _interval = 1f;
    /// <summary>ランダムにオブジェクトを生成する範囲を指定する（左下）</summary>
    [SerializeField] Vector2 _lowerLeft = new Vector2(-8, -4);
    /// <summary>ランダムにオブジェクトを生成する範囲を指定する（右上）</summary>
    [SerializeField] Vector2 _upperRight = new Vector2(8, 4);
    float _timer = 0;

    void Start()
    {
        for (int i = 0; i < _initialCount; i++)
        {
            Generate();
        }   // 最初に、設定された数のオブジェクトを生成する
    }

    void Update()
    {
        // タイマー処理をして一定時間おきにオブジェクトを生成する
        _timer += Time.deltaTime;

        if (_timer > _interval)
        {
            _timer = 0;
            Generate();
        }
    }

    /// <summary>
    /// 設定されたオブジェクトをランダムな場所に一つ生成する
    /// </summary>
    void Generate()
    {
        float x = Random.Range(_lowerLeft.x, _upperRight.x);
        float y = Random.Range(_lowerLeft.y, _upperRight.y);
        Instantiate(_prefab, new Vector2(x, y), Quaternion.identity, this.transform);
    }
}
