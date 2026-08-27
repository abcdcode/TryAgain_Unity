public class CharacterStat : IReplayable
{
    public CharacterStat()
    {
    }
    public void GameUpdate()
    {
    }

    public void LateGameUpdate()
    {
    }

    public void Load(SaveData data)
    {
    }

    public void Save(SaveData data)
    {
    }
    public float MoveSpeed => BaseMoveSpeed;
    private const float BaseMoveSpeed = 1000;
}