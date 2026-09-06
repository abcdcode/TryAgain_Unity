using UnityEngine;

public class AIState_MoveAndAttack : DefaultEnemyAIState<EnemyAI_DefaultMoveShot>
{
    public override void Init(Enemy p)
    {
        base.Init(p);
        owner.Angle = 180;
        m_curCool = AIData.m_shotCool;
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        owner.MoveForward(AIData.m_speed);
        m_curCool -= Time.deltaTime;
        if (m_curCool <= 0)
        {
            var b = BulletContainer.Instance.Create(BulletDB.EnemyTestBullet, true);
            b.InitPos(owner.Position);
            b.LookAt(GameManager.Instance.CurPlayer.Position);
            b.damageInfo = new DamageInfo(){dmg = 1, faction = FactionEnum.Enemy};
            b.SetSize(new Vector2(30, 30));
            m_curCool = AIData.m_shotCool;
        }
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(m_curCool);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        m_curCool = data;
    }
    private float m_curCool;
}