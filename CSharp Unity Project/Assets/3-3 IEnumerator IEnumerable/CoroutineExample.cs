using System.Collections;
using UnityEngine;

/// <summary>
/// コルーチンを使った例
/// </summary>
public class CoroutineExample : MonoBehaviour
{
    Coroutine _coroutine = default;

    public void PrimeCheck(int n)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(PrimeCheckRoutine(n));
            Debug.Log("PrimeCheck starts.");
        }
        else
        {
            Debug.Log("PrimeCheck is running.");
        }
    }

    /// <summary>
    /// 素数判定処理のコルーチン
    /// </summary>
    /// <param name="n">判定対象の自然数</param>
    /// <returns></returns>
    IEnumerator PrimeCheckRoutine(int n)
    {
        bool isPrime = true;

        if (n < 2)
        {
            isPrime = false;
        }
        else if (n == 2)
        {
            isPrime = true;
        }
        else if (n % 2 == 0)
        {
            isPrime = false;
        }
        else
        {
            for (int i = 3; i * i <= n; i += 2)
            {
                // Debug.Log(i);    // 途中経過を確認したい時はアンコメントする
                yield return null;

                if (n % i == 0)
                {
                    isPrime = false;
                }
            }
        }

        if (isPrime)
        {
            Debug.Log($"{n} は素数です。");
        }
        else
        {
            Debug.Log($"{n} は合成数です。");
        }
    }
}
