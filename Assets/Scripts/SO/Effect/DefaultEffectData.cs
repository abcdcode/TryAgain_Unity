using UnityEngine;
public class DefaultEffectData : EffectDataSO
{
    [SerializeField]private float m_time;
    [SerializeField]private int m_animId;
    [SerializeField]private bool m_isLoop;
    public float Time => m_time;
    public int AnimId => m_animId;
    public bool IsLoop => m_isLoop;
}