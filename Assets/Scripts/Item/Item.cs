public abstract class Item : IReplayable
{
    public virtual void Init(Player owner)
    {
        
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
    public Player owner;
}