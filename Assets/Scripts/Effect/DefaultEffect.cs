using UnityEngine;

public class DefaultEffect : Effect
{
    public override void Init(EffectDataSO so)
    {
        base.Init(so);
        if(so is DefaultEffectData d)
        {
            m_Data = d;
            m_time = 0;
            Animator.SetAnim(d.AnimId,d.IsLoop);
        }
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        m_time += Time.deltaTime;
        if(m_time >= m_Data.Time)
        {
            Delete();
        }
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(m_time);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        m_time = data;
    }
    private DefaultEffectData m_Data;
    private float m_time;
}