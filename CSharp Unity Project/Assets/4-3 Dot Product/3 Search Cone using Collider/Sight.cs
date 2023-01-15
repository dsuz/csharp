using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 索敵コンポーネント
/// 視界を設定するトリガーと同じオブジェクトにアタッチして使う
/// </summary>
public class Sight : MonoBehaviour
{
    [SerializeField] Text _message;

    void Start()
    {
        _message.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _message.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _message.enabled = false;
        }
    }
}
