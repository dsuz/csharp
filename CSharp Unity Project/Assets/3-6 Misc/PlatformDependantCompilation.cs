using UnityEngine;

public class PlatformDependantCompilation : MonoBehaviour
{
    // �ȉ��̂悤�ɂ��āA�v���b�g�t�H�[�����ƂɈႤ�R�[�h�����s���邱�Ƃ��ł���
#if UNITY_STANDALONE_WIN
    void Start()
    {
        
    }
#endif

#if UNITY_IOS
    void Start()
    {
        
    }
#endif

#if UNITY_ANDROID
    void Start()
    {
        
    }
#endif
}
