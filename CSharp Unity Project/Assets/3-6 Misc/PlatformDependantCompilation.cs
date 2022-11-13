using UnityEngine;

public class PlatformDependantCompilation : MonoBehaviour
{
    // 以下のようにして、プラットフォームごとに違うコードを実行することができる
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
