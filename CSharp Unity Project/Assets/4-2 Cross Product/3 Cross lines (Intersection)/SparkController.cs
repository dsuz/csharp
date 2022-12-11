using UnityEngine;

/// <summary>
/// オブジェクトが破棄された時にエフェクトとなるプレハブを生成するコンポーネント
/// </summary>
public class SparkController : MonoBehaviour
{
    /// <summary>破棄された時のエフェクト</summary>
    [SerializeField] GameObject _effectPrefab = default;

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            Instantiate(_effectPrefab, this.transform.position, Quaternion.identity);
        }   // 破棄された時にエフェクトを出すが、シーンがアンロードされてオブジェクトが破棄された時にはエフェクトを出さない
    }
}
