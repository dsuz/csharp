using System.Collections;
using UnityEngine;

/// <summary>
/// IEnumerable がどういうものか説明するためのサンプル
/// </summary>
public class WhatIsIEnumerable : MonoBehaviour
{
    public void Run()
    {
        // foreach ループ内で、関数内で次の yield return まで実行し、yield return がなくなったらループをぬける
        foreach (var item in IEnumerableExample())
        {
            // なにもしない
        }
    }

    public IEnumerable IEnumerableExample()
    {
        Debug.Log("ここまで実行した 1");
        yield return null;
        Debug.Log("ここまで実行した 2");
        yield return null;
        Debug.Log("ここまで実行した 3");
        yield return null;
        Debug.Log("ここまで実行した 4");
        yield return null;

        for (int i = 5; i < 10; i++)
        {
            Debug.Log($"ここまで実行した {i}");
            yield return null;
        }
    }
}
