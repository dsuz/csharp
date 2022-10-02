using UnityEngine;

/// <summary>
/// パワーアップアイテムを制御する。
/// 取ると連射できる弾の数が一つ増える。
/// </summary>
public class PowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SingletonSystem.Instance.BulletsInScene++;
            Destroy(this.gameObject);
        }
    }
}
