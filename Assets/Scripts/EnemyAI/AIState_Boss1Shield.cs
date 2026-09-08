using UnityEngine;

public class AIState_Boss1Shield : EnemyAIState
{
    public override void AIInit(params object[] parmater)
    {
        base.AIInit(parmater);
        bossid = (int)parmater[0];
        m_curAngle = 90;
        SetPos();
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        m_curAngle += RotateSpeed * Time.deltaTime;
        SetPos();
    }
    public void SetPos()
    {
        var boss = EnemyContainer.Instance.GetList().Find(x => x.IndexId == bossid);
        if(boss == null)
        {
            owner.Delete();
            return;
        }
        owner.Angle = m_curAngle + 90;
        owner.Position = boss.Position + CalcUtils.LookDir(m_curAngle).normalized * (boss.GetSize().x/2+owner.GetSize().x);
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(bossid);
        data.Write(m_curAngle);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        bossid = data;
        m_curAngle = data;
    }
    private float m_curAngle;
    private int bossid;
    private const float RotateSpeed = 100;
}