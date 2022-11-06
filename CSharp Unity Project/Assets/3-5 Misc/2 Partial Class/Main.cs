using UnityEngine;

public partial class Main : MonoBehaviour
{
    [SerializeField] string _message = "Message";

    void Start()
    {
        Debug.Log("Main クラスの Start() が実行されました");
    }

    partial void PartialMethod();   // パーシャル メソッドの宣言
}
