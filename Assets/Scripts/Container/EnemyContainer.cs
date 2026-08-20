public class EnemyContainer : ReplayObjContainer<Enemy>
{
    public override int ConvertId(string id)
    {
        return EnemyDB.Instance.ConvertId(id);
    }

    public override string ConvertId(int id)
    {
        return EnemyDB.Instance.ConvertId(id);
    }
    public static Enemy EnemyBuild(string id, string aiId)
    {
        var e = Instance.Create(id,true);
        var ai = EnemyAIDB.Instance.GetData(aiId);
        e.AIInit(ai);
        return e;
    }
    public override Enemy Create(string id, bool isIdCounting)
    {
        var d = EnemyDB.Instance.GetData(id);
        if(d == null) return null;
        var e = Instantiate(d.m_Prefab).GetComponent<Enemy>();
        e.Init(d);
        e.ObjId = d.m_Id;
        if(isIdCounting)
        {
            e.IndexId = GetNextId();
        }
        Items.Add(e);
        return e;
    }
}