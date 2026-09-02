using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CoolActiveItem : ActiveItem
{
    public override void Init(ItemDataSO data)
    {
        base.Init(data);
        m_coolData = (DefaultCoolActiveItemDataSO)data;
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        cool -= Time.deltaTime;
        if(cool <= 0) cool = 0;
    }
    public override float CoolRemainRate()
    {
        return cool/m_coolData.Cool;
    }
    public override void OnUse()
    {
        base.OnUse();
        if(cool <= 0)
        {
            Active();
            cool = m_coolData.Cool;
        }
    }
    public virtual void Active()
    {
        
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(cool);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        cool = data;
    }
    protected float cool;
    protected DefaultCoolActiveItemDataSO m_coolData;
}