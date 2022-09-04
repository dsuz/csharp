using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // DOTween を使うため

public class GaugeController : MonoBehaviour
{
    /// <summary>ゲージを変化させる秒数</summary>
    [SerializeField] float _changeValueInterval = 0.5f;
    Slider _slider = default;

    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    /// <summary>
    /// ゲージを減らす
    /// </summary>
    /// <param name="value">増減させる量（割合）</param>
    public void Change(float value)
    {
        ChangeValue(_slider.value + value);
    }

    /// <summary>
    /// ゲージを満タンにする
    /// </summary>
    public void Fill()
    {
        ChangeValue(1f);
    }

    /// <summary>
    /// 指定された値までゲージを滑らかに変化させる
    /// </summary>
    /// <param name="value"></param>
    void ChangeValue(float value)
    {
        // DOTween.To() を使って連続的に変化させる
        DOTween.To(() => _slider.value, // 連続的に変化させる対象の値
            x => _slider.value = x, // 変化させた値 x をどう処理するかを書く
            value, // x をどの値まで変化させるか指示する
            _changeValueInterval);   // 何秒かけて変化させるか指示する
    }
}
