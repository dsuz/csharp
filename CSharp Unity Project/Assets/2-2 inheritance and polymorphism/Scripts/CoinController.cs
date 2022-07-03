using UnityEngine;

/// <summary>
/// 得点のためのコインアイテムのコンポーネント
/// </summary>
public class CoinController : ItemBase2D   // ItemBase2D を継承している
{
    /// <summary>取った時に加点する値</summary>
    [SerializeField] int _score = 100;

    public override void Activate()
    {
        FindObjectOfType<GameManagerSecondTermFifthWeek>().AddScore(_score);
    }
}
