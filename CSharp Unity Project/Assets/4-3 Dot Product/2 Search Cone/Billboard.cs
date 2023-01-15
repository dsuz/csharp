using UnityEngine;

/// <summary>
/// World Space に置いた Canvas(UI) が常にカメラの方に向くよう制御するコンポーネント
/// Canvas もしくは UI のオブジェクトに追加して使う
/// </summary>
public class Billboard : MonoBehaviour
{
    void Update()
    {
        this.transform.forward = Camera.main.transform.forward;
    }
}
