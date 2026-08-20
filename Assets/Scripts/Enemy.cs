using System.Diagnostics;

public class Enemy : ReplayMono, IHitable
{
    public void Init(EnemyDataSO data)
    {
        EnemyData = data;
    }
    public void AIInit(EnemyAIDataSO aiData)
    {
        EnemyAIData = aiData;
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        EnemyData?.Save(data,this);
        EnemyAIData?.Save(data,this);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        EnemyData?.Load(data,this);
        EnemyAIData?.Load(data,this);
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        BulletContainer.HitCheck(this);
        EnemyData?.GameUpdate(this);
        EnemyAIData?.GameUpdate(this);
        
    }
    public override void Delete()
    {
        
    }
    public void TakeDamage(DamageInfo dmg)
    {
        ReplayDebug.Log("TakeDamage!!!");
    }

    public float HP{get;private set;}

    public FactionEnum Faction => FactionEnum.Enemy;

    public float HitSize => this.GetSize().x;

    public EnemyDataSO EnemyData;
    public EnemyAIDataSO EnemyAIData;
}