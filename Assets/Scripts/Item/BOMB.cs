public class BOMB : CoolActiveItem
{
    public override void Active()
    {
        base.Active();
        foreach(var e in EnemyContainer.Instance.GetList())
        {
            e.TakeDamage(damageInfo);
        }
        foreach(var b in BulletContainer.Instance.GetList())
        {
            if(b.damageInfo.faction == FactionEnum.Player) continue;
            var ef = EffectContainer.Instance.Create("EnemyDead",true);
            ef.Position = b.Position;
            ef.SetSize(b.GetSize());
            b.Delete();
        }
    }
    private static DamageInfo damageInfo = new DamageInfo(){dmg = 100, faction = FactionEnum.Player};
}