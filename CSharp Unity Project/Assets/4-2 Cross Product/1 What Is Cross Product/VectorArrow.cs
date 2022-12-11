using UnityEngine;

/// <summary>
/// ベクトルの矢印を移動・回転する
/// </summary>
public class VectorArrow : MonoBehaviour
{
    [SerializeField] Transform _arrow;

    void Update()
    {
        if (_arrow)
        {
            _arrow.up = transform.position;
        }
    }
}
