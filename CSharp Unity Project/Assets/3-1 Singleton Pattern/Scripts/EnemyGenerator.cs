using System.Collections;
using UnityEngine;

/// <summary>
/// 敵を生成するコンポーネント
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    /// <summary>生成する敵のプレハブ</summary>
    [SerializeField] GameObject _enemyPrefab = default;
    /// <summary>敵を生成する間隔（秒）</summary>
    [SerializeField] float _interval = 5f;
    /// <summary>敵を生成する場所</summary>
    [SerializeField] Transform[] _spawnPoints = default;

    void Start()
    {
        StartCoroutine(GenerateRoutine());
    }

    IEnumerator GenerateRoutine()
    {
        while (true)
        {
            System.Array.ForEach(_spawnPoints, t => Instantiate(_enemyPrefab, t.position, Quaternion.identity));
            yield return new WaitForSeconds(_interval);
        }
    }
}
