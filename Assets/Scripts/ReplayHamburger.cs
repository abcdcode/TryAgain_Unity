using System.Collections.Generic;

public class ReplayHamburger : Singleton<ReplayHamburger>
{
    public void Reset()
    {
        saveDic.Clear();
    }
    public void Save(int frame, SaveData data)
    {
        if(saveDic.Count > frame)
        {
            saveDic[frame] = data;
        }
        else
        {
            while(saveDic.Count <= frame)
            {
                saveDic.Add(data);
            }
        }
    }
    public SaveData Load(int frame)
    {
        return saveDic[frame];
    }
    private List<SaveData> saveDic = new List<SaveData>();
}