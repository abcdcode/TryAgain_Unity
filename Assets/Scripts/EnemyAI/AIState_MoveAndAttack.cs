using UnityEngine;

public class AIState_MoveAndAttack : EnemyAIState
{
    public override void Init(Enemy p)
    {
        base.Init(p);
        m_ai = AIData<EnemyAI_MoveLeftAndAttack>();
        owner.Angle = 180;
        m_curCool = m_ai.m_shotCool;
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        owner.MoveForward(m_ai.m_speed);
        m_curCool -= Time.deltaTime;
        if (m_curCool <= 0)
        {
            var b = BulletContainer.Instance.Create(BulletDB.EnemyTestBullet, true);
            b.InitPos(owner.Position);
            b.LookAt(GameManager.Instance.CurPlayer.Position);
            b.damageInfo = new DamageInfo(){dmg = 1, faction = FactionEnum.Enemy};
            b.SetSize(new Vector2(30, 30));
            m_curCool = m_ai.m_shotCool;
        }
    }
    private EnemyAI_MoveLeftAndAttack m_ai;
    private float m_curCool;
}