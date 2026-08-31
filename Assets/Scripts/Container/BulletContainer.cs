using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class BulletContainer : ReplayObjContainer<Bullet>
{
    public static void HitCheck(IHitable a)
    {
        if(a.Obj == null) return;
        
        foreach(var b in BulletContainer.Instance.GetList())
        {
            if(b.damageInfo.faction == a.Faction) continue;
            if(Bullet.HitCheck(b,a.Obj.Position,a.Obj.GetSize().x/2))
            {
                a.TakeDamage(b.damageInfo);
                b.Delete();
            }
        }
    }
    public static void HitCheckNew(IHitable a)
    {
        if(a.Obj == null) return;
        var cX = GameManager.ScreenX/CellCount;
        var cY = GameManager.ScreenY/CellCount;
        var aPos = a.Obj.Position;
        int YY = (int)(aPos.y+GameManager.ScreenY/2)/cY;
        int XX = (int)(aPos.x+GameManager.ScreenX/2)/cX;
        foreach(var b in GetNearCellBullets(XX,YY))
        {
            if(!b.isActiveAndEnabled) continue;
            if(b.damageInfo.faction == a.Faction) continue;
            if(Bullet.HitCheck(b,aPos,a.Obj.GetSize().x/2))
            {
                a.TakeDamage(b.damageInfo);
                b.Delete();
            }
        }
    }
    public override ushort ConvertId(string id)
    {
        return BulletDB.Instance.ConvertId(id);
    }

    public override string ConvertId(ushort id)
    {
        return BulletDB.Instance.ConvertId(id);
    }

    public override Bullet Create(string id, bool isIdCounting)
    {
        var data = BulletDB.Instance.GetData(id);
        Bullet b;
        //b = Instantiate(m_BulletPrefab).GetComponent<Bullet>();
        
        if(m_bulletPool.Count > 0)
        {
            b = m_bulletPool.Dequeue();
            b.gameObject.SetActive(true);
        }
        else
        {
            b = Instantiate(m_BulletPrefab).GetComponent<Bullet>();
        }
        
        
        b.Init(data);
        b.ObjId = data.m_Id;
        if(isIdCounting)
        {
            b.IndexId = GetNextId();
        }
        Items.Add(b);
        return b;
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        SettingCell();
    }
    public override void Clear()
    {
        base.Clear();
        ClearCell();
    }
    private static void ClearCell()
    {
        for(int x = 0 ; x < CellCount ; x++)
        {
            for(int y = 0; y < CellCount; y++)
            {
                if(cell[y,x] == null) cell[y,x] = new Cell<Bullet>();
                cell[y,x].Clear();
            }
        }
    }
    public override void Delete(Bullet t)
    {
        //base.Delete(t);
        
        Items.Remove(t);
        t.gameObject.SetActive(false);
        m_bulletPool.Enqueue(t);
        
        
    }
    private static void SettingCell()
    {
        ClearCell();
        foreach(var b in Instance.GetList())
        {
            var cX = GameManager.ScreenX/CellCount;
            var cY = GameManager.ScreenY/CellCount;
            int YY = (int)(b.Position.y+GameManager.ScreenY/2)/cY;
            int XX = (int)(b.Position.x+GameManager.ScreenX/2)/cX;
            if(XX < 0 || XX >= CellCount || YY < 0 || YY >= CellCount) continue;
            cell[YY,XX].Add(b);
        }
    }
    private static List<Bullet> m_getNearBullets = new List<Bullet>();
    private static List<Bullet> GetNearCellBullets(int x, int y)
    {
        m_getNearBullets.Clear();
        var l = GetCellBullets(x-1,y-1);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        l = GetCellBullets(x,y-1);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        l = GetCellBullets(x+1,y-1);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        l = GetCellBullets(x-1,y);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        l = GetCellBullets(x,y);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        l = GetCellBullets(x+1,y);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        l = GetCellBullets(x-1,y+1);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        l = GetCellBullets(x,y+1);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        l = GetCellBullets(x+1,y+1);
        if(l.Count > 0)m_getNearBullets.AddRange(l);
        return m_getNearBullets;
    }
    private static List<Bullet> GetCellBullets(int x, int y)
    {
        if(x < 0 || x >= CellCount || y < 0 || y >= CellCount) return new ();
        if(cell[y,x] == null) return new ();
        return cell[y,x].values;
    }
    private const int CellCount = 20;
    private static Cell<Bullet>[,] cell = new Cell<Bullet>[CellCount,CellCount];
    [SerializeField]private GameObject m_BulletPrefab;
    private Queue<Bullet> m_bulletPool = new Queue<Bullet>();
    public class Cell<T>
    {
        public Cell()
        {
            values = new List<T>(InitSize);
        }
        public void Add(T t)
        {
            values.Add(t);
        }
        public void Clear()
        {
            values.Clear();
        }
        public List<T> Get()
        {
            return values.ToList();
        }
        public List<T> values;
        private const int InitSize = 64; 
    }
}