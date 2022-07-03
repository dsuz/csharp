using UnityEngine;

/// <summary>
/// カギアイテムのコンポーネント
/// ドアを開ける
/// </summary>
public class KeyController : ItemBase2D   // ItemBase2D を継承している
{
    /// <summary>ドアのアニメーション</summary>
    [Tooltip("ドアを開けるアニメーションを再生するための Animator コンポーネントを持つ GameObject を指定する")]
    [SerializeField] Animator _door = default;
    /// <summary>ドアを開けるアニメーションのステート名</summary>
    [Tooltip("ドアを開けるアニメーションのステート名を指定する")]
    [SerializeField] string _stateName = "";
    /// <summary>ドアを開ける音</summary>
    [Tooltip("ドアを開ける音を指定する")]
    [SerializeField] AudioClip _doorOpeningSound = default;

    public override void Activate()
    {
        _door.Play(_stateName);
        AudioSource.PlayClipAtPoint(_doorOpeningSound, Camera.main.transform.position);
    }
}
