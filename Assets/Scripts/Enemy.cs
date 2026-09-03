using System.Diagnostics;

public class Enemy : ReplayMono, IHitable
{
    public void Init(EnemyDataSO data)
    {
        EnemyData = data;
        EnemyData.Init(this);
        HP = EnemyData.Hp;
        Animator?.SetAnim(1,true);
    }
    public void AIInit(EnemyAIDataSO aiData)
    {
        EnemyAIData = aiData;
        EnemyAIData.Init(this);
        EnemyAIState = EnemyAIData.BuildAIState(this);
        EnemyAIState.Init(this);
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        EnemyData.Save(data,this);
        data.Write(EnemyAIDB.Instance.ConvertId(EnemyAIData.m_Id));
        EnemyAIData.Save(data,this);
        EnemyAIState.Save(data);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        EnemyData.Load(data,this);
        ushort aid = data;
        if(EnemyAIData == null || EnemyAIDB.Instance.ConvertId(EnemyAIData.m_Id) != aid)
        {
            EnemyAIData = EnemyAIDB.Instance.GetData(aid);
        }
        if(EnemyAIState == null)
        {
            EnemyAIState = EnemyAIData.BuildAIState(this);
            EnemyAIState.Init(this);
        }
        EnemyAIData.Load(data,this);
        EnemyAIState.Load(data);
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        BulletContainer.HitCheckNew(this);
        EnemyData?.GameUpdate(this);
        EnemyAIData?.GameUpdate(this);
        EnemyAIState?.GameUpdate();
        
    }
    public override void Delete()
    {
        EnemyContainer.Instance.Delete(this);
    }
    public override void ExecuteCool(int id)
    {
        base.ExecuteCool(id);
        EnemyAIData.ExecuteCool(this,id);
    }
    public void TakeDamage(DamageInfo dmg)
    {
        if(dmg == null)
        {
            ReplayDebug.Log("Null DMG Check!");
        }
        //ReplayDebug.Log("TakeDamage!!!");
        HP -= dmg.dmg;
        if(HP <= 0)
        {
            var e = EffectContainer.Instance.Create("EnemyDead",true);
            e.SetSize(this.GetSize());
            e.Position = this.Position;
            Delete();
        }
    }

    public float HP{get;private set;}

    public FactionEnum Faction => FactionEnum.Enemy;

    public float HitSize => this.GetSize().x;

    public EnemyDataSO EnemyData;
    public EnemyAIDataSO EnemyAIData;
    public EnemyAIState EnemyAIState;
}