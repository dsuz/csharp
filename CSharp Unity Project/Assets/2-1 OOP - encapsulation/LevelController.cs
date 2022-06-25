using UnityEngine;

/// <summary>
/// プレイヤーのパラメーターを管理するコンポーネント
/// </summary>
public class LevelController : MonoBehaviour
{
    /// <summary>レベルアップテーブルを読み込むため</summary>
    [SerializeField] LevelManager m_levelManager = default;
    /// <summary>プレイヤーのレベル</summary>
    public int m_level = 1;
    /// <summary>プレイヤーのパラメーター</summary>
    public PlayerStats m_playerStats = default;

    void Start()
    {
        ReloadData();
    }

    /// <summary>
    /// レベルに対してプレイヤーのパラメーターを読み込み直す
    /// </summary>
    public void ReloadData()
    {
        PlayerStats stats = m_levelManager.GetData(m_level);

        if (stats.Level != 0)
        {
            m_playerStats = m_levelManager.GetData(m_level);
        }
    }

    /// <summary>
    /// レベルアップする
    /// </summary>
    /// <param name="level">レベルアップさせたいレベル数</param>
    public void LevelUp(int level = 1)
    {
        PlayerStats stats = m_levelManager.GetData(m_level + level);

        if (stats.Level != 0)
        {
            m_level += level;
            m_playerStats = m_levelManager.GetData(m_level);
        }
    }
}
