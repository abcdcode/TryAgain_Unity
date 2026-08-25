using UnityEngine;

public class EnemyAI_MoveLeftAndAttack : EnemyAIDataSO
{
    [SerializeField]private float m_speed = 500;
    [SerializeField]private float m_shotCool = 1;
    public override void Init(Enemy enemy)
    {
        base.Init(enemy);
        enemy.Angle = 180;
        enemy.m_CoolTimer.SetCool(Shoot,m_shotCool,0,true);
    }
    public override void GameUpdate(Enemy obj)
    {
        base.GameUpdate(obj);
        obj.MoveForward(m_speed);
    }
    public override void ExecuteCool(Enemy obj, int value)
    {
        base.ExecuteCool(obj, value);
        if(value == Shoot)
        {
            var b = BulletContainer.Instance.Create(BulletDB.EnemyTestBullet,true);
            b.InitPos(obj.Position);
            b.LookAt(GameManager.Instance.CurPlayer.Position);
            b.Faction = FactionEnum.Enemy;
            b.SetSize(new Vector2(30,30));
            obj.m_CoolTimer.SetCool(Shoot,m_shotCool,0,true);
        }
    }
    private const int Shoot = 10;
}