public class EnemyContainer : ReplayObjContainer<Enemy>
{
    public static void PlayerHitCheck(IHitable a)
    {
        if(a.Obj == null) return;
        
        foreach(var b in EnemyContainer.Instance.GetList())
        {
            if(CalcUtils.SegmentCircle(b.Position,a.Obj.Position,(a.Obj.GetSize().x+b.GetSize().x)/2))
            {
                a.TakeDamage(new DamageInfo(){dmg = 1, faction = FactionEnum.Enemy});
            }
        }
    }
    public override ushort ConvertId(string id)
    {
        return EnemyDB.Instance.ConvertId(id);
    }

    public override string ConvertId(ushort id)
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