public class CoolRefresh : OnceItem
{
    public override void Active()
    {
        base.Active();
        var list = Inventory.GetActives();
        foreach(var l in list)
        {
            if(l is CoolActiveItem c)
            {
                c.ResetCool();
            }
        }
    }
}