using Unity.VisualScripting;
using UnityEngine;

public class Bullet : ReplayMono
{
    public static bool HitCheck(Bullet b, Vector2 target, float tSize)
    {
        return CalcUtils.SegmentCircle(b.Position,b.prevPos,target,tSize);
    }
    public override void Awake()
    {
        base.Awake();
        renderer = GetComponent<SpriteRenderer>();
        
    }
    public void Init(BulletSO d)
    {
        m_Data = d;
        renderer.sprite = m_Data.m_spriteSO.m_Sprite;
        m_Data.Init(this);
    }
    public void InitPos(Vector2 pos)
    {
        Position = pos;
        prevPos = pos;
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(prevPos);
        var id = BulletDB.Instance.ConvertId(m_Data.m_Id);
        data.Write(id);
        m_Data.Save(data,this);
        
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        prevPos = data;
        int id = data;
        if(m_Data == null || BulletDB.Instance.ConvertId(m_Data.m_Id) != id)
        {
            var newData = BulletDB.Instance.GetData(id);
            m_Data = newData;
        }
        m_Data.Load(data,this);
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        m_Data.GameUpdate(this);
    }
    public override void Delete()
    {
        BulletContainer.Instance.Delete(this);
    }
    public override void ExecuteCool(int id)
    {
        m_Data.ExecuteCool(this,id);
        //Debug.Log($"Bullet ExecuteCool : {id}");
        BulletCoolEnum e = (BulletCoolEnum)id;
        if(e == BulletCoolEnum.Destroy)
        {
            Delete();
            return;
        }
    }
    public DamageInfo damageInfo;
    public FactionEnum Faction{get;set;}
    private Vector2 prevPos;
    private SpriteRenderer renderer;
    public BulletSO m_Data;
}
public enum BulletCoolEnum
{
    Destroy
}