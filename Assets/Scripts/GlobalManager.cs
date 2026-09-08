using System.IO;
using UnityEngine;

public class GlobalManager : SingletonBehavior<GlobalManager>
{
    public override void Awake()
    {
        base.Awake();
        savePath = $"{Application.persistentDataPath}/run.dat";
        savePathJ = $"{Application.persistentDataPath}/runJ.json";
        DontDestroyOnLoad(gameObject);
    }
    public bool IsSaveExist()
    {
        return File.Exists(savePath) && File.Exists(savePathJ);
    }
    public void RemoveSave()
    {
        if(!IsSaveExist()) return;
        File.Delete(savePath);
        File.Delete(savePathJ);
    }
    public static string savePath;
    public static string savePathJ;
}