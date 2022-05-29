using UnityEngine;

public class CSOnUnity : MonoBehaviour
{
    string s = "member variable";

    void Start()
    {
        Debug.Log($"{name} : GameObject の名前");
        Debug.Log($"{tag} : GameObject の Tag");
        Debug.Log($"{transform.position} : GameObject の座標");
        Debug.Log($"{this.s} : メンバー変数");
        this.Test();
    }

    void Test()
    {
        Debug.Log("メソッドを呼んでみた");
    }
}
