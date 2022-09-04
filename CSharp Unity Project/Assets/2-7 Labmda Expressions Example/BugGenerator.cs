using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定したプレハブを1体ずつ生成し続けるコンポーネント
/// </summary>
public class BugGenerator : MonoBehaviour
{
    [SerializeField] GameObject _bugPrefab;
    [SerializeField] Transform[] _spawnPoints;
    GameObject _go;

    void Update()
    {
        if (!_go && _spawnPoints.Length > 0)
        {
            var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            _go = Instantiate(_bugPrefab);
            _go.transform.position = spawnPoint.position;
        }
    }
}
