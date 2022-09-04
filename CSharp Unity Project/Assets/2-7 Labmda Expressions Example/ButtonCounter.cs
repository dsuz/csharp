using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ButtonCounter : MonoBehaviour
{
    /// <summary>ボタンが何回押されたら Particle を再生するか</summary>
    [SerializeField] int _buttonCount = 3;
    /// <summary>再生する Particle</summary>
    [SerializeField] ParticleSystem _particle = default;

    void Start()
    {
        Button b = GetComponent<Button>();
        // ボタンに「_buttonCount 回クリックしたら Particle を再生する」機能を追加する
        b?.OnClickAsObservable()
            .Buffer(_buttonCount)   // 何回溜まったら Subscribe が発動するか指定する
            .Subscribe(_ => _particle?.Play()); // 発動する処理を書く

        // 上記の "_" は「ラムダ式の引数を（使わないので）受け取らずに破棄する」という意味です。
        // C# 7.0 以降で使うことができます。
        // 参照: 破棄 https://docs.microsoft.com/ja-jp/dotnet/csharp/fundamentals/functional/discards

        // ついでに「a キーを _buttonCount 回押したらしたら Particle を再生する」処理を追加する
        var keyDown = this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.A))
            .Buffer(_buttonCount)
            .Subscribe(_ => _particle?.Play());
    }
}
