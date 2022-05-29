using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// break の使用例として、素数を判定する。
/// </summary>
public class BreakExample : MonoBehaviour
{
    /// <summary>結果を表示する UI</summary>
    [SerializeField] Text _text;

    /// <summary>
    /// 素数を判定する。外部から呼び出して使う。
    /// </summary>
    /// <param name="inputField">数字が文字列として入っている UI</param>
    public void IsPrime(InputField inputField)
    {
        // 入力した文字列が整数に変換できるかチェックする
        if (int.TryParse(inputField.text, out _))
        {
            // 整数に変換できる場合
            int n = int.Parse(inputField.text);
            // 素数を判定する。
            IsPrime(n);
        }
        else
        {
            // 入力された文字列が整数として不正だった場合
            _text.text = "int の範囲で 整数を入力して下さい";
        }
    }

    /// <summary>
    /// 素数を判定して結果を表示する。
    /// 単純な「エラトステネスのふるい」を使って素数を判定している。
    /// 与えられた数が 2 未満の場合は考慮していないので注意すること。
    /// </summary>
    /// <param name="number">素数かどうか判定する値</param>
    void IsPrime(int number)
    {
        bool isPrime = true; // 素数であると仮定して処理を始める

        // 2, 3, 4, 5, ... で順番に割り算をしていく
        for (int i = 2; i < number; i++)
        {
            if (number % i == 0) // 割り切れたら素数ではない
            {
                _text.text = $"{number} は {i} で割り切れる 素数ではない";
                isPrime = false;    // 素数ではない事が確定した
                break;  // これ以上処理をしても無駄なのでループを抜ける（break の使用例）
            }
        }

        // 素数だったらメッセージを表示する
        if (isPrime)
        {
            _text.text = $"{number} は素数である";
        }
    }
}
