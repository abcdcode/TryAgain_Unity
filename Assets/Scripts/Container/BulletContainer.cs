using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletContainer : ReplayObjContainer<Bullet>
{
    public static void HitCheck(IHitable a)
    {
        if(a.Obj == null) return;
        
        foreach(var b in BulletContainer.Instance.GetList())
        {
            if(b.Faction == a.Faction) continue;
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
        int YY = (int)(a.Obj.Position.y+GameManager.ScreenY/2)/cY;
        int XX = (int)(a.Obj.Position.x+GameManager.ScreenX/2)/cX;
        foreach(var b in GetNearCellBullets(XX,YY))
        {
            if(b.Faction == a.Faction) continue;
            if(Bullet.HitCheck(b,a.Obj.Position,a.Obj.GetSize().x/2))
            {
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
    public override void GameUpdate()
    {
        base.GameUpdate();
        SettingCell();
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
    private static void SettingCell()
    {
        ClearCell();
        foreach(var b in Instance.GetList())
        {
            var cX = GameManager.ScreenX/CellCount;
            var cY = GameManager.ScreenY/CellCount;
            int YY = (int)(b.Position.y+GameManager.ScreenY/2)/cY;
            int XX = (int)(b.Position.x+GameManager.ScreenX/2)/cX;
            if(XX < 0 || XX >= CellCount || YY < 0 || YY > CellCount) continue;
            cell[YY,XX].Add(b);
        }
    }
    private static List<Bullet> GetNearCellBullets(int x, int y)
    {
        List<Bullet> result = new List<Bullet>();
        result.AddRange(GetCellBullets(x-1,y-1));
        result.AddRange(GetCellBullets(x,y-1));
        result.AddRange(GetCellBullets(x+1,y-1));
        result.AddRange(GetCellBullets(x-1,y));
        result.AddRange(GetCellBullets(x,y));
        result.AddRange(GetCellBullets(x+1,y));
        result.AddRange(GetCellBullets(x-1,y+1));
        result.AddRange(GetCellBullets(x,y+1));
        result.AddRange(GetCellBullets(x+1,y+1));
        return result;
    }
    private static List<Bullet> GetCellBullets(int x, int y)
    {
        if(x < 0 || x >= CellCount || y < 0 || y > CellCount) return new ();
        if(cell[y,x] == null) return new ();
        return cell[y,x].Get();
    }
    private const int CellCount = 10;
    private static Cell<Bullet>[,] cell = new Cell<Bullet>[CellCount,CellCount];
    [SerializeField]private GameObject m_BulletPrefab;
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