using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ReplayHamburger : Singleton<ReplayHamburger>
{
    public void Reset()
    {
        saveDic.Clear();
    }
    public void Save(int frame, SaveData data)
    {
        saveDic[frame] = data;
    }
    public SaveData Load(int frame)
    {
        return saveDic[frame];
    }
    public void WriteAsSaveFile(int frame)
    {
        ReplayDebug.Log($"Save At {frame}");
        var s = saveDic[frame];
        File.WriteAllBytes(GlobalManager.savePath,s.data);
        RunState rs = new RunState();
        rs.Frame = frame;
        rs.ReplayGauge = GameManager.Instance.CurPlayer.Stat.ReplayGauge;
        var json = JsonUtility.ToJson(rs);
        File.WriteAllText(GlobalManager.savePathJ,json);
    }
    public bool LoadInSaveFile()
    {
        if(!File.Exists(GlobalManager.savePath)) return false;
        var b = File.ReadAllBytes(GlobalManager.savePath);
        var json = File.ReadAllText(GlobalManager.savePathJ);
        var rs = JsonUtility.FromJson<RunState>(json);
        SaveData data = new SaveData();
        data.Write(b);
        data.Save();
        Save(rs.Frame,data);
        GameManager.Instance.FileLoad(data,rs.Frame);

        GameManager.Instance.CurPlayer.Stat.ReplayGauge = rs.ReplayGauge;
        EnemyContainer.Instance.Clear();
        return true;
    }
    private Dictionary<int,SaveData> saveDic = new Dictionary<int,SaveData>();
}
[Serializable]
public class RunState
{
    public int Frame;
    public float ReplayGauge;
}