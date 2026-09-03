public abstract class OnceItem : Item
{
    public override void OnEquip()
    {
        base.OnEquip();
        Active();
        Delete();
    }
    public virtual void Active()
    {
        
    }
}