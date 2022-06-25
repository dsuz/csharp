using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステータスが表示されているウィンドウを管理する
/// </summary>
public class StatusWindowManager : MonoBehaviour
{
    [SerializeField] Text m_statusWindow = default;
    [SerializeField] LevelController m_player = default;
    int m_lastUpdateLevel = 0;

    void Update()
    {
        // レベルが変わったら再描画する
        if (m_player.m_level != m_lastUpdateLevel)
        {
            Refresh();
            m_lastUpdateLevel = m_player.m_level;
        }
    }

    /// <summary>
    /// ウィンドウを再描画する
    /// </summary>
    public void Refresh()
    {
        // C# の標準ライブラリで文字列を組み立てる
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        PlayerStats s = m_player.m_playerStats;
        builder.AppendLine(s.Level.ToString());
        builder.AppendLine(s.Maxhp.ToString());
        builder.AppendLine(s.Maxmp.ToString());
        builder.AppendLine(s.Attack.ToString());
        builder.AppendLine(s.Magic.ToString());
        builder.AppendLine(s.Dex.ToString());
        m_statusWindow.text = builder.ToString();
    }
}
