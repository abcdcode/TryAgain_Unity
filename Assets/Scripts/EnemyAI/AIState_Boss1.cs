using UnityEngine;

/// <summary>
/// 보스 AI
/// 전방으로 천천히 전진하면서 계속 잡몹을 소환
/// 잡몹 종류
/// 1.전방으로 아무렇게나 전진하는 고기방패
/// 2.보스 주변을 빙빙 도는 진짜 방패
/// </summary>
public class AIState_Boss1 : EnemyAIState
{
    public override void Init(Enemy p)
    {
        base.Init(p);
        owner.Angle = 180;
        pCool = 4;
        
    }
    public override void AIInit(params object[] parmater)
    {
        base.AIInit(parmater);
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        MainUIManager.Instance.SetBoss(owner.IndexId);
        owner.MoveForward(moveSpeed);
        pCool -= Time.deltaTime;
        if(pCool <= 0)
        {
            var v = SeedManager.Instance.GetFloat(0,1);
            if(v > 0.5f)
            {
                SummonBirdStrike();
                pCool = 1;
            }
            else
            {
                SummonShielder();
                pCool = 2;
            }
        }
    }
    public override void OnDead()
    {
        base.OnDead();
    }
    public Enemy SummonBirdStrike()
    {
        var e = EnemyContainer.EnemyBuild(EnemyDB.TestEnemy,EnemyAIDB.Kamikaze);
        e.Position = owner.Position;
        return e;
    }
    public Enemy SummonShielder()
    {
        var e = EnemyContainer.EnemyBuild(EnemyDB.TestEnemy,"Boss1Shield");
        e.EnemyAIState.AIInit(owner.IndexId);
        return e;
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(pCool);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        pCool = data;
    }
    private float pCool;
    private const float moveSpeed = 100;
}