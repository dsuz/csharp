using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームを管理するコンポーネント
/// ライフと得点、それらの UI を制御する
/// ゲーム内に一つだけ存在させること。
/// </summary>
public class GameManagerSecondTermFifthWeek : MonoBehaviour
{
    /// <summary>最大ライフ</summary>
    [SerializeField] int _maxLife = 100;
    /// <summary>初期ライフ</summary>
    [SerializeField] int _initialLife = 10;
    /// <summary>ライフゲージ</summary>
    [SerializeField] Slider _lifeGauge = default;
    /// <summary>スコアを表示するテキスト</summary>
    [SerializeField] Text _scoreText = default;

    int _score = 0;
    int _life = 0;

    void Start()
    {
        _life = _initialLife;
        AddLife(0);
        AddScore(0);
    }

    /// <summary>
    /// 得点を追加し、表示を更新する。
    /// </summary>
    /// <param name="score">加算したい得点。負の値を渡すと減点する。得点表示の更新だけをしたい時は 0 を渡す。</param>
    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString("D8");
    }

    /// <summary>
    /// ライフを回復し、表示を更新する。
    /// </summary>
    /// <param name="life">回復したいライフ。負の値を渡すとライフが減る。ライフ表示の更新だけをしたい時は 0 を渡す。</param>
    public void AddLife(int life)
    {
        _life += life;
        _lifeGauge.value = (float)_life / _maxLife;
    }
}
