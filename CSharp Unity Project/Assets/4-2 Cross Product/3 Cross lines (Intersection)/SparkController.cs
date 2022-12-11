using UnityEngine;

public class SparkController : MonoBehaviour
{
    [SerializeField] GameObject m_effectPrefab = default;

    private void OnDestroy()
    {
        Instantiate(m_effectPrefab, this.transform.position, Quaternion.identity);
    }
}
