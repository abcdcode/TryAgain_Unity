using UnityEngine;

public class EnemyAI_StayAndAttack : EnemyAI_DefaultMoveShot
{
    public override EnemyAIState BuildAIState(Enemy enemy)
    {
        return new AIState_StayAndAttack();
    }
}