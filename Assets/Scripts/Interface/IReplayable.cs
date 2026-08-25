public interface IReplayable
{
    public void Save(SaveData data);
    public void Load(SaveData data);
    public void GameUpdate();
    public void LateGameUpdate();
}