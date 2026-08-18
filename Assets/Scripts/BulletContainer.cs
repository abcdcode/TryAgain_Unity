using UnityEngine;

public class BulletContainer : ReplayObjContainer<Bullet>
{
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
        if(data.m_Prefab == null)
        {
            Bullet b = Instantiate(m_BulletPrefab).GetComponent<Bullet>();
        }
        return null;
    }
    [SerializeField]private GameObject m_BulletPrefab;
}