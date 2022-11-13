using UnityEngine;

/// <summary>
/// 爆発エフェクトを制御するコンポーネント
/// </summary>
public class ExplosionController : MonoBehaviour
{
    ParticleSystem[] m_particleSystems = default;
    AudioSource m_audio = default;

    void Start()
    {
        m_particleSystems = this.transform.GetComponentsInChildren<ParticleSystem>();
        m_audio = GetComponent<AudioSource>();
        Explode(Vector3.up * -100f, 0f); // 見えない所で一回爆発させて初期化する
    }

    /// <summary>
    /// 指定した位置で爆発させる
    /// </summary>
    /// <param name="position">爆発する座標</param>
    /// <param name="volume">爆発音の音量</param>
    public void Explode(Vector3 position, float volume = 1f)
    {
        this.transform.position = position;

        foreach (var p in m_particleSystems)
        {
            p.Play();
        }

        if (m_audio)
        {
            m_audio.volume = volume;
            m_audio.Play();
        }
    }

    /// <summary>
    /// Button から爆発させるためのテスト用メソッド
    /// </summary>
    /// <param name="positionY"></param>
    public void ExplodeTest(float positionY)
    {
        Explode(Vector3.up * positionY);
    }
}
