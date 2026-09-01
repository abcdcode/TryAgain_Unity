using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CoolActiveItem : ActiveItem
{
    public override void Init(ItemDataSO data)
    {
        base.Init(data);
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        cool -= Time.deltaTime;
    }
    public override float CoolRemainRate()
    {
        return cool/m_coolData.Cool;
    }
    public virtual void Active()
    {
        
    }
    protected float cool;
    protected DefaultCoolActiveItemDataSO m_coolData;
}