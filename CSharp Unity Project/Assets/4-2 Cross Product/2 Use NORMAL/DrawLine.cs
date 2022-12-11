using UnityEngine;

/// <summary>
/// ドラッグで線を描くコンポーネント
/// </summary>
public class DrawLine : MonoBehaviour
{
    /// <summary>線を描くために使う Line Renderer</summary>
    [SerializeField] LineRenderer _line;
    /// <summary>線を描く時に、各点をどれくらい離すか</summary>
    [SerializeField] float _distanceBetweenPoints = 0.1f;
    /// <summary>線を描いた後にその線に沿ってオブジェクトを動かすコンポーネント</summary>
    [SerializeField] MoveOnLine _moveOnLine;
    /// <summary>線を描いている間は true になる</summary>
    bool _isDrawing = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDrawing = true;
            Reset();
            _moveOnLine.Stop();
        }   // ボタンを押し下げた時
        else if (Input.GetMouseButtonUp(0))
        {
            _isDrawing = false;
            // Line Renderer の positions を取得し、MoveOnLine コンポーネントを初期化する
            Vector3[] positions = new Vector3[_line.positionCount];
            _line.GetPositions(positions);
            _moveOnLine.Init(positions);
        }   // ボタンを離した時

        if (_isDrawing)
        {
            Draw();
        }
    }

    /// <summary>
    /// 線を描く座標をすべて消して、画面上の線を消す
    /// </summary>
    void Reset()
    {
        _line.positionCount = 0;
    }

    /// <summary>
    /// マウスの座標に線を描く
    /// </summary>
    void Draw()
    {
        // Line Renderer の positions に新たに追加する座標を計算する
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        // 最後に追加した Line Renderer の positions よりある程度離れていたら、その座標を Line Renderer に追加する
        if (_line.positionCount == 0 || (Vector3.Distance(pos, _line.GetPosition(_line.positionCount - 1)) > _distanceBetweenPoints))
        {
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, pos);
        }
    }
}
