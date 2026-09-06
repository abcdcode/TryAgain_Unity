public abstract class DefaultEnemyAIState<T> : EnemyAIState where T : EnemyAIDataSO
{
    protected T AIData => owner.EnemyAIData as T;
}