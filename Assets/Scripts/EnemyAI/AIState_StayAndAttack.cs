using UnityEngine;

public class AIState_StayAndAttack : DefaultEnemyAIState<EnemyAI_DefaultMoveShot>
{

    public override void Init(Enemy p)
    {
        base.Init(p);
        m_curCool = AIData.m_shotCool;
        isMoving = true;
    }
    public override void AIInit(params object[] parmater)
    {
        base.AIInit(parmater);
        finalPos = (Vector2)parmater[0];
        owner.LookAt(finalPos);
    }

    public override void GameUpdate()
    {
        base.GameUpdate();
        if(isMoving)
        {
            owner.Position = Vector2.MoveTowards(owner.Position,finalPos,AIData.m_speed*Time.deltaTime);
        }
        if(owner.Position == finalPos)
        {
            isMoving = false;
        }
        if(!isMoving)
        {
            owner.LookAt(GameManager.Instance.CurPlayer.Position);
            m_curCool -= Time.deltaTime;
            if(m_curCool <= 0)
            {
                AIData.m_attackInfo.Shoot(owner);
                m_curCool = AIData.m_shotCool;
            }
        }
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(m_curCool);
        data.Write(finalPos);
        data.Write(isMoving);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        m_curCool = data;
        finalPos = data;
        isMoving = data;
    }
    private bool isMoving;
    private Vector2 finalPos;
    private float m_curCool;
}