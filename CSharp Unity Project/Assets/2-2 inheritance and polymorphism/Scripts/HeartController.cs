using UnityEngine;

/// <summary>
/// ライフを回復する（減らす）アイテムのコンポーネント
/// </summary>
public class HeartController : ItemBase2D   // ItemBase2D を継承している
{
    /// <summary>ライフを回復（減少）させる値</summary>
    [SerializeField] int _recoverLife = 10;

    public override void Activate()
    {
        FindObjectOfType<GameManagerSecondTermFifthWeek>().AddLife(_recoverLife);
    }
}
