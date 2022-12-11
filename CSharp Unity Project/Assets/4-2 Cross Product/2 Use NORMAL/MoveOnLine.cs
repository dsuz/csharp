// 日本語対応済
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MoveOnLine : MonoBehaviour
{
    [SerializeField] float _speed = 0.2f;
    [SerializeField] float _stoppingDistance = Mathf.Epsilon;
    [SerializeField] Transform _target;
    [SerializeField] float _rotateSpeed = 1.0f;
    Vector3[] _positions;
    int _targetIndex = 0;
    Vector3 _targetUpDirection;
    bool _isWorking = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!_isWorking) return;

        if (Vector3.Distance(this.transform.position, _positions[_targetIndex]) < _stoppingDistance)
        {
            if (_targetIndex < _positions.Length - 1)
            {
                _targetIndex++;
                _target.position = _positions[_targetIndex];
                Vector3 dir = _target.position - _positions[_targetIndex - 1];
                _targetUpDirection = Vector3.Cross(dir.normalized, Vector3.back);
            }
            else
            {
                _isWorking = false;
            }
        }
        else
        {
            Vector3 dir = _target.position - this.transform.position;   // 進行方向
            this.transform.Translate(_speed * Time.deltaTime * dir.normalized, Space.World);
        }

        if (_targetUpDirection != Vector3.zero)
        {
            this.transform.up = Vector3.Slerp(this.transform.up, _targetUpDirection, Time.deltaTime * _rotateSpeed);
        }
    }

    public void Init(Vector3[] positions)
    {
        _positions = positions;
        _targetIndex = 0;
        this.transform.position = _positions[_targetIndex];
        _isWorking = true;
    }

    public void Stop()
    {
        _isWorking = false;
    }
}
