using UnityEngine;

/// <summary>
/// null の説明として、Sprite の色を変える。
/// </summary>
public class NullExample : MonoBehaviour
{
    /// <summary>変える先の色</summary>
    [SerializeField] Color _color = Color.red;

    /// <summary>
    /// Sprite の色を変える。外部から呼び出して使う。
    /// </summary>
    public void ChangeColor()
    {
        // SpriteRenderer コンポーネントを取得する。追加されてない場合は null が戻される。
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _color;
    }

    /// <summary>
    /// Sprite の色を変える。外部から呼び出して使う。
    /// エラーにならないように null チェックをしている。
    /// </summary>
    public void ChangeColorWithNullCheck()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // null チェック
        if (spriteRenderer != null) // 変数が Unity のコンポーネントである場合は if (spriteRenderer) と書いてもよい
        {
            // null じゃなければ色を変える
            spriteRenderer.color = _color;
        }
        else
        {
            // null だったらインスタンスを操作せず、警告を出力する
            Debug.LogWarning($"{name} に Sprite Renderer コンポーネントが追加されていません");
        }
    }
}
