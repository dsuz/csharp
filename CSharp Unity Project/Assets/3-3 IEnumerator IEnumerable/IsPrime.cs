using UnityEngine;

/// <summary>
/// 非同期処理などはまったく使わない、通常の素数判定処理
/// </summary>
public class IsPrime : MonoBehaviour
{
    /// <summary>
    /// 与えられた n に対して素数かどうか判定し、結果を Console に出力する。
    /// </summary>
    /// <param name="n"></param>
    public void PrimeChecker(int n)
    {
        if (PrimeCheck(n))
        {
            Debug.Log($"{n} は素数です");
        }
        else
        {
            Debug.Log($"{n} は合成数です");
        }
    }

    bool PrimeCheck(int n)
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

                if (n % i == 0)
                {
                    isPrime = false;
                }
            }
        }

        return isPrime;
    }
}
