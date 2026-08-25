public class IdCountManager : SingletonBehavior<IdCountManager>, IReplayable
{
    public int GetNextId()
    {
        return id++;
    }
    public void Save(SaveData data)
    {
        data.Write(id);
    }

    public void Load(SaveData data)
    {
        id = data;
    }

    public void GameUpdate()
    {
    }

    public void LateGameUpdate()
    {
    }

    private int id = 0;
}