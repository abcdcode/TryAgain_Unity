using UnityEngine;
public class SeedManager : SingletonBehavior<SeedManager>, IReplayable
{
    public int GetInt(int min, int max)
    {
        var r = Random.Range(min,max);
        SetSeed();
        return r;
    }
    public float GetFloat(float min, float max)
    {
        var r = UnityEngine.Random.Range(min,max);
        SetSeed();
        return r;
    }
    public void GameUpdate()
    {
    }
    public void InitSeed(int v)
    {
        m_CurSeed = v;
        Random.InitState(m_CurSeed);
    }
    private void SetSeed()
    {
        m_CurSeed = Random.Range(0,1000000000);
        Random.InitState(m_CurSeed);
    }
    public void LateGameUpdate()
    {
    }

    public void Load(SaveData data)
    {
        m_CurSeed = data;
    }

    public void Save(SaveData data)
    {
        data.Write(m_CurSeed);
    }
    private int m_CurSeed;
}