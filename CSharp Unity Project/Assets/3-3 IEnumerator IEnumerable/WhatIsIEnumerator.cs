using System.Collections;
using UnityEngine;

/// <summary>
/// IEnumerator がどういうものか説明するためのサンプル
/// </summary>
public class WhatIsIEnumerator : MonoBehaviour
{
    // 実行中の IEnumerator
    IEnumerator _enumerator = default;

    void Start()
    {
        // 実行開始
        _enumerator = IEnumeratorExample();
    }

    public void Run()
    {
        // MoveNext() により、次の yield return まで実行する
        if (!_enumerator.MoveNext())
        {
            Debug.Log("処理は完了しています。");
        }
    }

    IEnumerator IEnumeratorExample()
    {
        Debug.Log("ここまで実行した 1");
        yield return null;
        Debug.Log("ここまで実行した 2");
        yield return null;
        Debug.Log("ここまで実行した 3");
        yield return null;
        Debug.Log("ここまで実行した 4");
        yield return null;
        Debug.Log("ここまで実行した 5");
        yield return null;

        for (int i = 6; i < 10; i++)
        {
            Debug.Log($"ここまで実行した {i}");
            yield return null;
        }
    }
}
