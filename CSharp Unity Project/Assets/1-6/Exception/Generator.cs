using System;
using UnityEngine;

/// <summary>
/// プレハブを GameObnect として出現ポイントに生成する
/// </summary>
public class Generator : MonoBehaviour
{
    /// <summary>出現ポイント</summary>
    [SerializeField] Transform[] _spawnPoints;
    /// <summary>出現させるプレハブ</summary>
    [SerializeField] GameObject[] _prefabs;

    void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            var prefab = _prefabs[i % _prefabs.Length]; // 出現させるプレハブを取得する
            var spawnPoint = _spawnPoints[i];   // 出現ポイントを取得する
            // プレハブから GameObject を生成する（例外をスローする可能性がある処理）
            var go = Instantiate(prefab);
            // 生成した GameObject を出現ポイントに移動する（例外をスローする可能性がある処理）
            go.transform.position = spawnPoint.position;
        }
    }
}
