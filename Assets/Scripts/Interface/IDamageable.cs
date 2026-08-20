public interface IDamageable
{
    public void TakeDamage(DamageInfo dmg);
    public FactionEnum Faction{get;}
}
public struct DamageInfo
{
    public float dmg;
    public FactionEnum faction;
}