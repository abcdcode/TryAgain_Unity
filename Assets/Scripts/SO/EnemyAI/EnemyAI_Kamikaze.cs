using UnityEngine;

public class EnemyAI_Kamikaze : EnemyAIDataSO
{
    [SerializeField]private float m_speed;
    public override void Init(Enemy enemy)
    {
        base.Init(enemy);
        var vec = GameManager.Instance.CurPlayer.Position - enemy.Position;
        enemy.SetAngle(vec);
    }
    public override void GameUpdate(Enemy obj)
    {
        base.GameUpdate(obj);
        obj.MoveForward(m_speed);
    }
}