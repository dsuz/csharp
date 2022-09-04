using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UniRx;

/// <summary>
/// 虫のキャラクターを動かすためのコンポーネント
/// </summary>
public class BugController : MonoBehaviour, IPointerClickHandler
{
    // DOTween に関するパラメータ
    [Tooltip("横方向の動きを制御する"), SerializeField] float _endValueX = 8f;
    [Tooltip("縦方向の動きを制御する"), SerializeField] float _endValueY = 1f;
    [Tooltip("横方向の動きのループ時間"), SerializeField] float _timeX = 3f;
    [Tooltip("縦方向の動きのループ時間"), SerializeField] float _timeY = 1f;
    // UniRx に関するパラメータ/プロパティ
    [Tooltip("初期ライフ"), SerializeField] int _maxLife = 3;
    private readonly ReactiveProperty<int> _life = new IntReactiveProperty();

    void Start()
    {
        // DOTween を使って動きを作る
        transform.DOMoveX(_endValueX, _timeX).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalMoveY(_endValueY, _timeY).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        // UniRx の準備
        _life.Value = _maxLife;
        _life.AddTo(this);
        // ライフが減った時に実行する処理を設定する
        this._life.Subscribe(_ => Debug.Log($"撃たれた! 残りライフ: {_life}"));  // 初回呼ばれることを避けたい場合は Skip メソッドを呼ぶ
    }

    void OnDestroy()
    {
        // DOTween を止める（止めないと警告が出る）
        DOTween.KillAll();
    }

    /// <summary>
    /// オブジェクトをクリックしたらライフを減らし、ライフが 0 になったら破棄する
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        this._life.Value--;

        if (this._life.Value < 1)
        {
            Destroy(this.gameObject);
        }
    }    
}
