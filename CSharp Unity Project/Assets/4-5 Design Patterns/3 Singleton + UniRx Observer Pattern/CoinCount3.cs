using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コイン獲得数表示を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(Text))]
public class CoinCount3 : MonoBehaviour
{
    Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "0";
        GameManager3.Instance.ObserveEveryValueChanged(gm => gm.CoinCount)  // ここでプロパティの監視を設定している
            .Subscribe(coinCount => _text.text = coinCount.ToString());     // ここで監視対象の値が変更された時に実行する処理を登録している
    }
}
