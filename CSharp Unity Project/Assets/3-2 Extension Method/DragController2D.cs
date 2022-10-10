using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// オブジェクトをドラッグで移動させるためのコンポーネント。このコンポーネントが追加されているオブジェクトをドラッグで移動させることができる。
/// このスクリプトを動かすには Main Camera に Physics 2D Raycaster をアタッチする必要がある。
/// オブジェクトがドラッグ中であるか判定するために、Order In Layer の値を変更してそれをドラッグ中のフラグとして利用している。
/// これによりドラッグ中のオブジェクトを手前に表示している。しかし、他のオブジェクトを操作していないためボタンを離した時に後ろに回ってしまう場合がある。
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DragController2D : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>ドラッグ中に設定する Order In Layer の値</summary>
    int _orderInLayerWhenDragging = 1;
    /// <summary>ドラッグしていない時に設定する Order In Layer の値</summary>
    int _orderInLayerDefault = 0;
    SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_sprite.sortingOrder == _orderInLayerWhenDragging)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _sprite.sortingOrder = _orderInLayerWhenDragging;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _sprite.sortingOrder = _orderInLayerDefault;
    }
}
