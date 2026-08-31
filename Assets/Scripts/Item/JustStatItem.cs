public class JustStatItem : PassiveItem
{
    private JustStatItemDataSO Jata => (JustStatItemDataSO)Data;
    public override float AllDamageMult()
    {
        return Jata.AllDmgMult;
    }
    public override float MainDamageMult()
    {
        return Jata.MainDmgMult;
    }
    public override float SubDamageMult()
    {
        return Jata.SubDmgMult;
    }
    public override float MoveSpeedMult()
    {
        return Jata.MoveMult;
    }
}