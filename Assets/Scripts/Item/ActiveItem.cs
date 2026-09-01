public abstract class ActiveItem : Item
{
    public virtual void OnUse()
    {
        
    }
    public virtual void OnChangeToThis()
    {
        
    }
    public virtual void OnChangeToOther()
    {
        
    }
    public virtual float CoolRemainRate()
    {
        return 0;
    }
}