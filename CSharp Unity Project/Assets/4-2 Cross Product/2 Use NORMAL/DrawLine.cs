// 日本語対応済
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] LineRenderer _line;
    [SerializeField] float _distanceBetweenPoints = 0.1f;
    [SerializeField] MoveOnLine _character;
    bool _isDrawing = false;
    //List<Vector3> _positions = new List<Vector3>();

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDrawing = true;
            Reset();
            _character.Stop();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDrawing = false;
            Vector3[] positions = new Vector3[_line.positionCount];
            _line.GetPositions(positions);
            _character.Init(positions);
        }

        if (_isDrawing)
        {
            Draw();
        }
    }

    void Reset()
    {
        _line.positionCount = 0;
    }

    void Draw()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        if (_line.positionCount == 0 || (Vector3.Distance(pos, _line.GetPosition(_line.positionCount - 1)) > _distanceBetweenPoints))
        {
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, pos);
        }
    }
}
