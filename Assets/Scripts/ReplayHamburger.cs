using System.Collections.Generic;

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
    private Dictionary<int,SaveData> saveDic = new Dictionary<int,SaveData>();
}