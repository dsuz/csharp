using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ドラッグで線をひき、線が閉じた時に内側にあるオブジェクト（コライダーが付いているもの）を破棄する。
/// 線が一度交差したらそれ以上は線を描かない。
/// </summary>
[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]
public class Enclosure : MonoBehaviour
{
    /// <summary>線を描く時に、各点をどれくらい離すか</summary>
    [SerializeField] float _distanceBetweenPoints = 0.1f;
    /// <summary>囲まれた内側に色をつけるための Mesh Filter</summary>
    [SerializeField] MeshFilter _meshFilter = default;
    /// <summary>囲まれた内側に色をつけるための Mesh</summary>
    Mesh _mesh = default;
    /// <summary>線を描くための Line Renderer</summary>
    LineRenderer _line = default;
    /// <summary>囲まれた内側の当たり判定をとるためのコライダー</summary>
    PolygonCollider2D _polyCol = default;
    /// <summary>描いた曲線が閉じている場合は true とする</summary>
    bool _isLineClosed = false;
    /// <summary>線が交差していると判定された時、一番最後の線分と、最初から何番目の線分が交差しているかの値をここに入れる</summary>
    int _crossPoint = 0;
    /// <summary>交点の座標</summary>
    Vector2 _crossPosition;
    /// <summary>線を描いている間は true になる</summary>
    bool _isDrawing = false;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _polyCol = GetComponent<PolygonCollider2D>();
        _line.positionCount = 0;    // 線を消す
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawLine();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            FinishDrawLine();
        }

        if (_isDrawing)
        {
            DrawLine();
        }
    }

    /// <summary>
    /// LineRenderer を初期化する
    /// </summary>
    void StartDrawLine()
    {
        _meshFilter.mesh = null;
        _line.positionCount = 0;
        _isLineClosed = false;
        _isDrawing = true;
    }

    /// <summary>
    /// マウスの位置に Line をひく
    /// </summary>
    void DrawLine()
    {
        if (_isLineClosed) return; // 線が閉じていたらそれ以上は線を描かない。Polygon Collder 2D で処理できなくなるし、負荷が高い。

        // Line Renderer の positions に新たに追加する座標を計算する
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        // 最後に追加した Line Renderer の positions よりある程度離れていたら、その座標を Line Renderer に追加する
        if (_line.positionCount == 0 || (Vector3.Distance(position, _line.GetPosition(_line.positionCount - 1)) > _distanceBetweenPoints))
        {
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, position);
        }

        // 線が閉じているかどうか調べる。閉じていたら交点座標を計算する。
        if (IsLineClosed(ref _crossPosition))
        {
            _isLineClosed = true;
        }
    }

    /// <summary>
    /// 線をひき終わった時に呼ぶ。
    /// 線を構成する座標のうち、「閉じている部分」を構成する座標から Polygon Collider 2D を作る。
    /// Polygon Collider 2D の内部にあるオブジェクトを破棄する。
    /// </summary>
    void FinishDrawLine()
    {
        _isDrawing = false;
        if (!_isLineClosed) return; // 線が閉じていない時は何もしない

        _polyCol.pathCount = _line.positionCount - _crossPoint;
        // 交差した線分以降の座標リスト、つまり閉じた線を構成する座標リストを抽出する
        var positions = new Vector3[_line.positionCount];
        _line.GetPositions(positions);
        var closurePoints = positions.Skip(_crossPoint - 1).Take(positions.Length - _crossPoint - 1).ToArray(); // 閉じた曲線を構成する座標リスト
        closurePoints[0] = (Vector3) _crossPosition;
        Paint(closurePoints);
        // Vector3[] を Vector2[] に変換しながら Polygon Collider の points に代入する
        _polyCol.points = Array.ConvertAll<Vector3, Vector2>(closurePoints.ToArray(), x => x);
        List<Collider2D> results = new List<Collider2D>();  // 囲まれた内側に入っているコライダーをこの変数に受け取る
        
        // Polygon Collider と重なっているオブジェクトを破棄する
        if (_polyCol.OverlapCollider(new ContactFilter2D(), results) > 0)
        {
            results.ForEach(x => Destroy(x.gameObject));
        }
    }

    /// <summary>
    /// 最後に引いた線分が、それまでに引いた線分のうちどれかと交差しているかどうか調べる
    /// https://www.google.com/search?q=%E7%B7%9A%E5%88%86+%E4%BA%A4%E5%B7%AE%E5%88%A4%E5%AE%9A+%E5%A4%96%E7%A9%8D
    /// </summary>
    /// <param name="crossPosition">線が閉じている時、その座標を受け取る変数</param>
    /// <returns>線が閉じていたら true を返す。そうでない時は false を返す。</returns>
    bool IsLineClosed(ref Vector2 crossPosition)
    {
        if (_line.positionCount < 4) return false;  // 4点以上ない場合は交差しない

        // 座標 c, d は「Line を構成する最後と、その直前の座標」つまり最後の線分の両端となる
        Vector2 d = _line.GetPosition(_line.positionCount - 1);
        Vector2 c = _line.GetPosition(_line.positionCount - 2);

        // これまでに作られた線分と交差しているかどうかを最初から順番に判定する
        for (int i = 0; i < _line.positionCount - 2; i++)
        {
            Vector2 a = _line.GetPosition(i);
            Vector2 b = _line.GetPosition(i + 1);
            Vector3 s1 = Vector3.Cross(b - a, c - a);
            Vector3 s2 = Vector3.Cross(b - a, d - a);
            Vector3 s3 = Vector3.Cross(d - c, b - c);
            Vector3 s4 = Vector3.Cross(d - c, a - c);

            if (s1.z * s2.z < 0 && s3.z * s4.z < 0)
            {
                _crossPoint = i;    // 「ここで交差した」という情報を保存する
                crossPosition = a + (b - a) * s4.magnitude / (s3.magnitude + s4.magnitude); // 交点座標を計算する（https://imagingsolution.blog.fc2.com/blog-entry-137.html の公式を使った）
                return true;
            }   // 交差している場合（交差判定の解説は https://nekojara.city/unity-line-segment-cross がわかりやすい）
        }

        return false;
    }

    /// <summary>
    /// 囲まれた内側の色を変えるために、座標のリストからメッシュを作る
    /// </summary>
    /// <param name="vertices">囲むための座標のリスト</param>
    void Paint(Vector3[] vertices)
    {
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
        _mesh.SetVertices(vertices);
        List<int> indices = new List<int>();

        for (int i = 1; i < vertices.Length - 1; i++)
        {
            indices.Add(0);
            indices.Add(i);
            indices.Add(i + 1);
        }

        _mesh.SetTriangles(indices, 0);
        _mesh.RecalculateBounds();
    }
}
