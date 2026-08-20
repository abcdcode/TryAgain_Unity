using UnityEngine;

public class BulletContainer : ReplayObjContainer<Bullet>
{
    public static void HitCheck(IHitable a)
    {
        //ReplayDebug.Log("Start HitCheck");
        if(a.Obj == null) return;
        //ReplayDebug.Log("Start HitCheck 2");
        foreach(var b in BulletContainer.Instance.GetList())
        {
            //ReplayDebug.Log("Try HitCheck 0");
            if(b.Faction == a.Faction) continue;
            //ReplayDebug.Log("Try HitCheck");
            if(Bullet.HitCheck(b,a.Obj.Position,a.Obj.GetSize().x))
            {
                //ReplayDebug.Log("Hit!");
                a.TakeDamage(b.damageInfo);
                b.Delete();
            }
        }
    }
    public override int ConvertId(string id)
    {
        return BulletDB.Instance.ConvertId(id);
    }

    public override string ConvertId(int id)
    {
        return BulletDB.Instance.ConvertId(id);
    }

    public override Bullet Create(string id, bool isIdCounting)
    {
        var data = BulletDB.Instance.GetData(id);
        Bullet b = Instantiate(m_BulletPrefab).GetComponent<Bullet>();
        b.Init(data);
        b.ObjId = data.m_Id;
        if(isIdCounting)
        {
            b.IndexId = GetNextId();
        }
        Items.Add(b);
        return b;
    }
    [SerializeField]private GameObject m_BulletPrefab;
}