public abstract class EnemyAIState : IReplayable
{
    public virtual void Init(Enemy p)
    {
        owner = p;
    }
    public virtual void GameUpdate()
    {
    }

    public virtual void LateGameUpdate()
    {
    }

    public virtual void Load(SaveData data)
    {
    }

    public virtual void Save(SaveData data)
    {
    }
    protected virtual T AIData<T>() where T : EnemyAIDataSO
    {
        return owner.EnemyAIData as T;
    }
    protected Enemy owner;
}