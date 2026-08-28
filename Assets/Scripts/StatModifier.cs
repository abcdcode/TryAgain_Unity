public abstract class StatModifier
{
    /// <summary>
    /// 이동속도 증감
    /// </summary>
    /// <returns></returns>
    public virtual float MoveSpeedMult()
    {
        return 1;
    }
    /// <summary>
    /// 모든 피해량 증감
    /// </summary>
    /// <returns></returns>
    public virtual float AllDamageMult()
    {
        return 1;
    }
    /// <summary>
    /// 메인 무기 피해량 증감
    /// </summary>
    /// <returns></returns>
    public virtual float MainDamageMult()
    {
        return 1;
    }
    /// <summary>
    /// 그외 피해량 증감
    /// </summary>
    /// <returns></returns>
    public virtual float SubDamageMult()
    {
        return 1;
    }
    /// <summary>
    /// 공격 적중 시 효과
    /// </summary>
    /// <param name="target"></param>
    /// <param name="dmg"></param>
    public virtual void OnGiveDamage(IHitable target, ref DamageInfo dmg)
    {
        
    }
}