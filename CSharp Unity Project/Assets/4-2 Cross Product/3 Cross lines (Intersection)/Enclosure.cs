using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ドラッグで線をひき、線が閉じた時に内側にあるオブジェクト（コライダーが付いているもの）を破棄する。
/// </summary>
[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]
public class Enclosure : MonoBehaviour
{
    [Tooltip("負荷を下げるためのフレームスキップ数")]
    [SerializeField, Range(0, 20)] int m_frameSkip = 15;
    [SerializeField] MeshFilter m_meshFilter = default;
    Mesh m_mesh = default;
    LineRenderer m_line = default;
    PolygonCollider2D m_polyCol = default;
    List<Vector3> m_positionList = new List<Vector3>();
    int m_frameSkipCounter = 0;
    bool m_isLineClosed = false;
    int m_crossPoint = 0;
    /// <summary>交点</summary>
    Vector2? m_crossPosition = null;

    void Start()
    {
        m_line = GetComponent<LineRenderer>();
        m_polyCol = GetComponent<PolygonCollider2D>();
        StartDrawLine();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_frameSkipCounter = 0;
            StartDrawLine();
        }
        else if (Input.GetButton("Fire1"))
        {
            m_frameSkipCounter++;

            if (m_frameSkipCounter >= m_frameSkip)
            {
                m_frameSkipCounter = 0;
                DrawLine();
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            FinishDrawLine();
        }
    }

    /// <summary>
    /// LineRenderer を初期化する
    /// </summary>
    void StartDrawLine()
    {
        m_meshFilter.mesh = null;
        m_positionList.Clear();
        m_line.positionCount = m_positionList.Count;
        m_line.SetPositions(m_positionList.ToArray());
        m_isLineClosed = false;
    }

    /// <summary>
    /// マウスの位置に Line をひく
    /// </summary>
    void DrawLine()
    {
        if (m_isLineClosed) return; // 線が閉じていたらそれ以上は線をひかない。Polygon Collder 2D で処理できなくなるし、負荷が高い。

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        m_positionList.Add(position);
        m_line.positionCount = m_positionList.Count;
        m_line.SetPositions(m_positionList.ToArray());

        // 線が閉じているかどうか調べる
        if (IsLineClosed(ref m_crossPosition))
        {
            m_isLineClosed = true;
        }
    }

    /// <summary>
    /// 線をひき終わった時に呼ぶ。
    /// 線を構成する座標のうち、「閉じている部分」を構成する座標から Polygon Collider 2D を作る。
    /// Polygon Collider 2D の内部にあるオブジェクトを破棄する。
    /// </summary>
    void FinishDrawLine()
    {
        if (!m_isLineClosed) return;

        m_polyCol.pathCount = m_positionList.Count - m_crossPoint;
        // 交差した線分以降の座標リスト、つまり閉じた線を構成する座標リストを抽出する
        var closurePoints = m_positionList.GetRange(m_crossPoint, m_positionList.Count - m_crossPoint - 1);
        closurePoints[0] = (Vector3) m_crossPosition;
        Paint(closurePoints.ToArray());
        // Vector3[] を Vector2[] に変換しながら points に代入する
        m_polyCol.points = Array.ConvertAll<Vector3, Vector2>(closurePoints.ToArray(), x => { return x; });
        ContactFilter2D filter = new ContactFilter2D();
        List<Collider2D> results = new List<Collider2D>();
        
        if (m_polyCol.OverlapCollider(filter, results) > 0)
        {
            // results の要素のうち、自分自身以外を破棄する
            results.ForEach(x =>
            {
                if (!x.gameObject.Equals(this.gameObject))
                    Destroy(x.gameObject);
            });
        }
    }

    /// <summary>
    /// 最後に引いた線分が、それまでに引いた線分のうちどれかと交差しているかどうか調べる
    /// https://www.google.com/search?q=%E7%B7%9A%E5%88%86+%E4%BA%A4%E5%B7%AE+%E5%88%A4%E5%AE%9A
    /// </summary>
    /// <param name="crossPosition">線が閉じている時、その座標を受け取る変数</param>
    /// <returns>線が閉じていたら true を返す。そうでない時は false を返す。</returns>
    bool IsLineClosed(ref Vector2? crossPosition)
    {
        if (m_line.positionCount < 4) return false;

        Vector2 d = m_line.GetPosition(m_line.positionCount - 1);
        Vector2 c = m_line.GetPosition(m_line.positionCount - 2);

        for (int i = 0; i < m_line.positionCount - 2; i++)
        {
            Vector2 a = m_line.GetPosition(i);
            Vector2 b = m_line.GetPosition(i + 1);
            Vector3 s1 = Vector3.Cross(b - a, c - a);
            Vector3 s2 = Vector3.Cross(b - a, d - a);
            Vector3 s3 = Vector3.Cross(d - c, b - c);
            Vector3 s4 = Vector3.Cross(d - c, a - c);

            if (s1.z * s2.z < 0 && s3.z * s4.z < 0)
            {
                m_crossPoint = i;
                //m_crossPosition = GetCrossPosition(a, b, c, d);
                crossPosition = a + (b - a) * s4.magnitude / (s3.magnitude + s4.magnitude);
                return true;
            }
        }

        return false;
    }

    //Vector2 GetCrossPosition(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    Vector2 GetCrossPosition(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        //float det = (p1.x - p2.x) * (p4.y - p3.y) - (p4.x - p3.x) * (p1.y - p2.y);
        //float t = ((p4.y - p3.y) * (p4.x - p2.x) + (p3.x - p4.x) * (p4.y - p2.y)) / det;
        //float x = t * p1.x + (1f - t) * p2.x;
        //float y = t * p1.y + (1f - t) * p2.y;
        //return new Vector2(x, y);
        float s1 = Vector3.Cross(d - c, a - c).magnitude;
        float s2 = Vector3.Cross(d - c, b - c).magnitude;
        //Vector2 crossPoint = a + (c - a) * s1 / (s1 + s2) + (d - a) * s1 / (s1 + s2);
        Vector2 crossPoint = a + (b - a) * s1 / (s1 + s2);
        return crossPoint;
    }

    void Paint(Vector3[] vertices)
    {
        //m_mesh = new Mesh();
        //m_line.BakeMesh(m_mesh);
        //m_meshFilter.mesh = m_mesh;

        m_mesh = new Mesh();
        m_meshFilter.mesh = m_mesh;
        m_mesh.SetVertices(vertices);
        List<int> indices = new List<int>();

        for (int i = 1; i < vertices.Length - 1; i++)
        {
            indices.Add(0);
            indices.Add(i);
            indices.Add(i + 1);
        }

        m_mesh.SetTriangles(indices, 0);
        m_mesh.RecalculateBounds();
    }

    /// <summary>
    /// Polygon triangulation (多角形の三角形分割)
    /// </summary>
    /// <param name="vertices"></param>
    void TriangulatePolygon(Vector3[] vertices)
    {
        // メッシュの頂点をセットする
        m_mesh = new Mesh();
        m_meshFilter.mesh = m_mesh;
        m_mesh.SetVertices(vertices);

        // これ以降で三角形を計算する
        List<int> indices = new List<int>();
        var vList = vertices.ToList();

        // 原点から一番遠い点を探す
        var fareastVertex = vList.OrderByDescending(v => v.sqrMagnitude).FirstOrDefault();
        int y = vList.IndexOf(fareastVertex);

        {
            // 「一番遠い点」とその両隣の頂点から成る三角形の外積ベクトルの方向を保存する
            int x = (y - 1) % vList.Count;
            int z = (y + 1) % vList.Count;
            Vector3 v1 = vList[x] - vList[y];
            Vector3 v2 = vList[y] - vList[z];
            var crossDirection1 = Vector3.Cross(v1, v2).z;

            // vList[x], vList[y], vList[z] から成る三角形の内部に他の頂点があるか調べる（内外判定）
            bool hasOtherVertexInTriangle = false;

            for (int i = 0; i < vList.Count; i++)
            {
                if (i == x || i == y || i == z) continue;

                var c1 = Vector3.Cross(vList[x] - vList[z], vList[i] - vList[x]).z;
                var c2 = Vector3.Cross(vList[z] - vList[y], vList[i] - vList[z]).z;
                var c3 = Vector3.Cross(vList[y] - vList[x], vList[i] - vList[y]).z;

                if ((c1 > 0 && c2 > 0 && c3 > 0) || (c1 < 0 && c2 < 0 && c3 < 0))
                {
                    hasOtherVertexInTriangle = true;
                }
            }

            if (hasOtherVertexInTriangle)
            {
                y = (y + 1) % vList.Count;
                x = (y - 1) % vList.Count;
                z = (y + 1) % vList.Count;
                v1 = vList[x] - vList[y];
                v2 = vList[y] - vList[z];
                var crossDirection2 = Vector3.Cross(v1, v2).z;

                //if (crossDirection1 * crossDirection2 < 0)
            }
            else
            {
                indices.Add(x);
                indices.Add(y);
                indices.Add(z);
                vList.RemoveAt(y);
            }
        }
    }
}
