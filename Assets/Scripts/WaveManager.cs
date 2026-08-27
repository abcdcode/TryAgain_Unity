using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : SingletonBehavior<WaveManager>, IReplayable
{
    public void SetTestWave()
    {
        CurWave = WavePreset.GetStage1Wave();
        WaveStart();
    }
    public void WaveStart()
    {
        m_time = 0;
    }
    public void GameUpdate()
    {
        if(CurWave == null) return;
        for(int i = m_wIndex; i < CurWave.DataList.Count;i++)
        {
            var w = CurWave.DataList[i];
            if(w.Time <= m_time)
            {
                w.Execute();
                m_wIndex+=1;
            }
            else
            {
                break;
            }
        }
        m_time += Time.deltaTime;
    }
    public void LateGameUpdate()
    {
        
    }

    public void Load(SaveData data)
    {
        m_time = data;
        m_wIndex = data;
    }

    public void Save(SaveData data)
    {
        data.Write(m_time);
        data.Write(m_wIndex);
    }
    public Wave CurWave{get;private set;}
    private int m_wIndex;
    private float m_time;
}
public class Wave : IEnumerable<WaveData>
{
    public List<WaveData> DataList = new List<WaveData>();
    public IEnumerator<WaveData> GetEnumerator()
    {
        return DataList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class WaveData
{
    public WaveData(string e, Vector2 pos, string ai, float t)
    {
        SummonEnemy = EnemyDB.Instance.ConvertId(e);
        SummonPos = pos;
        EnemyAI = EnemyAIDB.Instance.ConvertId(ai);
        Time = t;
    }
    public void Execute()
    {
        var eid = EnemyDB.Instance.ConvertId(SummonEnemy);
        var ai = EnemyAIDB.Instance.GetData(EnemyAI);
        var e = EnemyContainer.Instance.Create(eid,true);
        e.Position = SummonPos;
        e.AIInit(ai);
    }
    public ushort SummonEnemy;
    public Vector2 SummonPos;
    public ushort EnemyAI;
    public float Time;
}


public static class WavePreset
{
    public static Wave GetStage1Wave()
    {
        Wave result = new Wave();
        for(int i = 0; i < 100; i ++)
        {
            var vec = new Vector2(1200,SeedManager.Instance.GetFloat(-500,500));
            WaveData data = new(EnemyDB.TestEnemy,vec,EnemyAIDB.MoveAttack1,i*0.1f);
            result.DataList.Add(data);
        }
        return result;
    }
}