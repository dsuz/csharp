using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ドラッグした時に、スクリプトがアタッチされた GameObject をマウスの位置に動かす。
/// </summary>
public class DragController : MonoBehaviour
{
    /// <summary>
    /// ドラッグした時に、スクリプトがアタッチされた GameObject をマウスの位置に動かす。
    /// EventTrigger から呼ばれることを前提に作っている。
    /// </summary>
    /// <param name="bed">Base Event Data</param>
    public void OnDrag(BaseEventData bed)
    {
        PointerEventData ped = (PointerEventData) bed;
        Vector3 pos = Camera.main.ScreenToWorldPoint(ped.position);
        pos.z = 0;
        this.transform.position = pos;
    }
}
