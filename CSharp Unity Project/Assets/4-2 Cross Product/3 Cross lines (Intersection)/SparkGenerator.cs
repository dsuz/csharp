using UnityEngine;

public class SparkGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_prefab = default;
    [SerializeField] int m_initialCount = 50;
    [SerializeField] float m_interval = 1f;
    [SerializeField] Vector2 m_lowerLeft = new Vector2(-8, -4);
    [SerializeField] Vector2 m_upperRight = new Vector2(8, 4);
    float m_timer = 0;

    void Start()
    {
        for (int i = 0; i < m_initialCount; i++)
        {
            Generate();
        }
    }

    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0;
            Generate();
        }
    }

    void Generate()
    {
        float x = Random.Range(m_lowerLeft.x, m_upperRight.x);
        float y = Random.Range(m_lowerLeft.y, m_upperRight.y);
        Instantiate(m_prefab, new Vector2(x, y), Quaternion.identity, this.transform);
    }
}
