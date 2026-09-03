public class EnemyAIState : IReplayable
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
    private Enemy owner;
}