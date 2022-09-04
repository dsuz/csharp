using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // DOTween を使うため

public class ScoreController : MonoBehaviour
{
    /// <summary>スコアをカウントアップさせる秒数</summary>
    [SerializeField] float _scoreChangeInterval = 0.5f;
    Text _scoreText = default;
    /// <summary>カンストするスコア</summary>
    int _maxScore = 99999999;
    int _score = 0;

    void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    /// <summary>
    /// 得点を加算し、表示を更新する
    /// </summary>
    /// <param name="score">追加する点数</param>
    public void AddScore(int score)
    {
        int tempScore = _score; // 追加前のスコア
        _score = Mathf.Min(_score + score, _maxScore);  // _maxScore でカンストさせる

        // カンストしてなかったら得点表示を更新する
        if (tempScore != _maxScore)
        {
            // DOTween.To() を使って連続的に変化させる
            DOTween.To(() => tempScore, // 連続的に変化させる対象の値
                x => tempScore = x, // 変化させた値 x をどう処理するかを書く
                _score, // x をどの値まで変化させるか指示する
                _scoreChangeInterval)   // 何秒かけて変化させるか指示する
                .OnUpdate(() => _scoreText.text = tempScore.ToString("00000000"))   // 数値が変化する度に実行する処理を書く
                .OnComplete(() => _scoreText.text = _score.ToString("00000000"));   // 数値の変化が完了した時に実行する処理を書く

            // このように書いてもよい
            //DOTween.To(() => tempScore, x =>
            //{
            //    tempScore = x;
            //    _scoreText.text = tempScore.ToString("00000000");
            //}, _score, _scoreChangeInterval).OnComplete(() => _scoreText.text = _score.ToString("00000000"));
        }
    }
}
