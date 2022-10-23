using System.Collections;
using UnityEngine;

/// <summary>
/// コルーチンを使わずに IEnumerator のみを使った非同期処理の例。
/// 実際はコルーチンがあるのでこういう作り方はしない。
/// あくまでコルーチンがどういう仕組みなのか、なぜコルーチンの戻り値は IEnumerator なのかを説明するための処理である。
/// </summary>
public class AsyncWithoutCoroutine : MonoBehaviour
{
    /// <summary>処理中の IEnumerator</summary>
    IEnumerator _enumerator = null;

    void Update()
    {
        if (_enumerator != null)
        {
            // Update 内で MoveNext し、次の yield return まで処理を進める
            if (!_enumerator.MoveNext())
            {
                // 判定処理が終わったら処理中の IEnumerator をクリアする
                _enumerator = null;
            }

            /***********
             * （注）
             * Update() 内で MoveNext() を呼ぶ、ということは、Update() が呼ばれる度に
             * 次の yield return までしか処理が実行されない、ということである。
             * この例では、「Update() の度に素数判定のためのループを一回まわす」ということになる。
             * ループは一秒間にフレームレートの回数までしか回らなくなるので、このやり方だと
             * 素数判定が完了するまでの速度は普通に IEnumerator を使わない時よりも圧倒的に遅くなる。
             ***********/
        }
    }

    /// <summary>
    /// 非同期的に素数判定をする。
    /// </summary>
    /// <param name="n">判定対象の自然数</param>
    public void AsyncPrimeCheckWithoutCoroutine(int n)
    {
        if (_enumerator == null)    // 実行中でない時
        {
            // 素数判定を開始する
            _enumerator = PrimeCheck(n);
            Debug.Log("PrimeCheck starts.");
        }
        else
        {
            // 実行中である時は何もしない
            Debug.Log("PrimeCheck is running.");
        }
    }

    /// <summary>
    /// 素数判定処理
    /// </summary>
    /// <param name="n">判定対象の自然数</param>
    /// <returns></returns>
    IEnumerator PrimeCheck(int n)
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
                yield return null;  // MoveNext が呼ばれる度にここまで実行する

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
