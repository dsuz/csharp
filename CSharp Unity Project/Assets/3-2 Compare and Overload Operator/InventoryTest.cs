using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Inventory（持ち物）を操作するコンポーネント
/// </summary>
public class InventoryTest : MonoBehaviour
{
    /// <summary>持ち物のデータ。Key: Gear ID, Value: 持っている数</summary>
    Dictionary<int, int> _inventory = new Dictionary<int, int>();
    /// <summary>持ち物ウインドウのルート</summary>
    [SerializeField] Transform _inventoryWindow = default;
    /// <summary>持ち物データを表示するプレハブ</summary>
    [SerializeField] GearDataEntryController _inventoryDataEntryPrefab = default;

    void Start()
    {
        // （ダミーの）持ち物データを作る
        _inventory.Add(1, 3);
        _inventory.Add(3, 2);
        _inventory.Add(4, 1);
        _inventory.Add(5, 4);
    }

    /// <summary>
    /// 表示を更新する
    /// </summary>
    public void RefreshInventoryUI()
    {
        // 表示を全削除する
        foreach (Transform t in _inventoryWindow)   // 直下の全ての子オブジェクトをループしたい時はこのような指定方法がある
        {
            Destroy(t.gameObject);
        }

        // 持ち物データから表示を作る
        foreach (var k in _inventory.Keys)
        {
            var gearDataEntry = Instantiate(_inventoryDataEntryPrefab, _inventoryWindow);
            gearDataEntry.ItemName = DataLoader.GetGear(k).Name;
            gearDataEntry.ItemCount = _inventory[k];
        }
    }

    /// <summary>
    /// アイテムを合成する。
    /// 持ち物の中からランダムに２つのアイテムを選んで合成する。
    /// </summary>
    public void Fusion()
    {
        if (_inventory.Values.Sum() < 2)
        {
            Debug.Log("合成できる装備がありません");
            return;
        }
        
        // 合成するアイテムをランダムに選ぶ
        int id1 = _inventory.Keys.ToArray()[Random.Range(0, _inventory.Keys.Count)];
        Remove(id1);    // 持ち物から減らす
        int id2 = _inventory.Keys.ToArray()[Random.Range(0, _inventory.Keys.Count)];
        Remove(id2);    // 持ち物から減らす
        Gear gear1 = DataLoader.GetGear(id1);
        Gear gear2 = DataLoader.GetGear(id2);
        // アイテムを合成して持ち物に追加する
        Gear newGear = gear1 + gear2;   // 足し算で合成することができる
        Add(newGear.Id);
        // メッセージを出して UI を更新する
        string message = $"{gear1.Name} と {gear2.Name} を合成して {newGear.Name} ができました。";
        Debug.Log(message);
        RefreshInventoryUI();
    }

    /// <summary>
    /// 持ち物からアイテムを一つ減らす
    /// </summary>
    /// <param name="id">減らすアイテムの ID</param>
    void Remove(int id)
    {
        if (_inventory[id] > 1)
        {
            _inventory[id]--;
        }
        else
        {
            _inventory.Remove(id);
        }
    }

    /// <summary>
    /// 持ち物にアイテムを一つ追加する
    /// </summary>
    /// <param name="id">追加するアイテムの ID</param>
    void Add(int id)
    {
        if (_inventory.ContainsKey(id))
        {
            _inventory[id]++;
        }
        else
        {
            _inventory.Add(id, 1);
        }
    }
}
