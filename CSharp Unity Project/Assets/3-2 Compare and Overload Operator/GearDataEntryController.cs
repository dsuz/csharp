using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムを表示するスクリプト
/// </summary>
public class GearDataEntryController : MonoBehaviour
{
    /// <summary>アイテム名を表示するための Text</summary>
    [SerializeField] Text _itemName = default;
    /// <summary>持っている数を表示するための Text</summary>
    [SerializeField] Text _itemCount = default;

    /// <summary>
    /// 表示されているアイテム名
    /// </summary>
    public string ItemName
    {
        // 以下のようにラムダ式でプロパティを定義することができる
        get => _itemName.text;
        set => _itemName.text = value;
    }

    /// <summary>
    /// 表示されている所持数
    /// </summary>
    public int ItemCount
    {
        get
        {
            if (int.TryParse(_itemCount.text, out int itemCount))
            {
                return itemCount;
            }

            return 0;
        }

        set => _itemCount.text = value.ToString();
    }
}
