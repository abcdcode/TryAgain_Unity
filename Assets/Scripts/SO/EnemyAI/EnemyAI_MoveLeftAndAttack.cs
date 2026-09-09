using UnityEngine;

public class EnemyAI_MoveLeftAndAttack : EnemyAI_DefaultMoveShot
{
    public override EnemyAIState BuildAIState(Enemy enemy)
    {
        return new AIState_MoveAndAttack();
    }
    private const int Shoot = 10;
}