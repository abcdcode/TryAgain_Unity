public class Enemy : ReplayMono, IDamageable
{
    public void Init(EnemyDataSO data, EnemyAIDataSO ai)
    {
        EnemyData = data;
        EnemyAIData = ai;
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        EnemyData?.GameUpdate(this);
        EnemyAIData.GameUpdate(this);
    }
    public override void Delete()
    {
        
    }

    public void TakeDamage(DamageInfo dmg)
    {
        
    }

    public float HP{get;private set;}

    public FactionEnum Faction => FactionEnum.Enemy;

    public EnemyDataSO EnemyData;
    public EnemyAIDataSO EnemyAIData;
}