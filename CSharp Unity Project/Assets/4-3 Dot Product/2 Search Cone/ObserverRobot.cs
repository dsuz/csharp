using UnityEngine;

/// <summary>
/// 頭を動かすコンポーネント
/// IK を使い頭を動かし、_lookAtTarget の方向を見る
/// </summary>
public class ObserverRobot : MonoBehaviour
{
    [SerializeField] Transform _lookAtTarget;
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        _anim.SetLookAtPosition(_lookAtTarget.position);
        _anim.SetLookAtWeight(1);
    }
}
