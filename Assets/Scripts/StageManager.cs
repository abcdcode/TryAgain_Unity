public class StageManager : SingletonBehavior<StageManager>, IReplayable
{
    public void GameInit()
    {
        ReplayLimit = 0;
    }
    public void GameUpdate()
    {
    }

    public void LateGameUpdate()
    {
    }
    public void SetWave()
    {
        
    }
    public void Load(SaveData data)
    {
        ReplayLimit = data;
    }

    public void Save(SaveData data)
    {
        data.Write(ReplayLimit);
    }
    public int ReplayLimit{get;set;}

}
public enum StageState
{
    Playing,
    Reward
}