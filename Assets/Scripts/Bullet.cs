using Unity.VisualScripting;
using UnityEngine;

public class Bullet : ReplayMono
{
    public static bool HitCheck(Bullet b, Vector2 target, float tSize)
    {
        if(!b.isActiveAndEnabled) return false;
        return CalcUtils.SegmentCircle(b.Position,b.prevPos,target,tSize+b.GetSize().x/2 * b.m_Data.m_bulletSize);
    }
    public override void Awake()
    {
        base.Awake();
        
    }
    public void Init(BulletSO d)
    {
        m_CoolTimer.Clear();
        m_Data = d;
        m_rederer.sprite = m_Data.m_spriteSO.m_Sprite;
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
        prevPos = Position;
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        prevPos = data;
        ushort id = data;
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
        //Debug.Log("Delete");
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
    public override void OnDrawGizmos()
    {
        if(GameManager.Instance == null) return;
        if(GameManager.Instance.IsDebug)
        {
            Gizmos.DrawSphere(Position,this.GetSize().x/2 * m_Data.m_bulletSize);
        }
    }
    public DamageInfo damageInfo;
    public FactionEnum Faction{get;set;}
    private Vector2 prevPos;
    public BulletSO m_Data;
}
public enum BulletCoolEnum
{
    Destroy
}