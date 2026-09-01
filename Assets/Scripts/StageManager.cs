using System.Collections.Generic;

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
    public void StageInit(int stage)
    {
        CurStage = stage;
        if(CurStage == 1)
        {
            m_Wave = WavePreset.GetStage1Wave();
        }
        StartWave();
    }
    public void EndWave()
    {
        foreach(var b in BulletContainer.Instance.GetList())
        {
            b.Delete();
        }
        SState = StageState.Reward;
        var reward = ItemDB.GetReward(3,ItemGrade.Normal);
        UIRewardList.Instance.SetReward(reward);
        ReplayLimit = GameManager.Instance.CurFrame;
    }
    public void PickReward()
    {
        CurWaveNum += 1;
        StartWave();
    }
    public void StartWave()
    {
        SState = StageState.Playing;
        GameManager.Instance.State = GameManagerState.Playing;
        WaveManager.Instance.SetWave(m_Wave[CurWaveNum]);
    }
    public void Load(SaveData data)
    {
        ReplayLimit = data;
    }

    public void Save(SaveData data)
    {
        data.Write(ReplayLimit);
    }
    private List<Wave> m_Wave;
    public int ReplayLimit{get;set;}
    public StageState SState{get;set;}
    public int CurWaveNum{get;private set;}
    public int CurStage{get;private set;}
    public const int LastWave = 4;
    public const int LastStage = 1;

}
public enum StageState
{
    Playing,
    Reward
}