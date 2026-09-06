using UnityEngine;

public abstract class EnemyAI_DefaultMoveShot : EnemyAIDataSO
{
    [SerializeField]public AttackDataSO m_attackInfo;
    [SerializeField] public float m_speed = 500;
    [SerializeField] public float m_shotCool = 1;
}