using System.Collections.Generic;

public class Inventory : ReplayObjContainer<Item>
{
    public static List<PassiveItem> GetPassives()
    {
        List<PassiveItem> result = new List<PassiveItem>();
        foreach(var i in Instance.GetList())
        {
            if(i is PassiveItem p) result.Add(p);
        }
        return result;
    }
    public static List<ActiveItem> GetActives()
    {
        List<ActiveItem> result = new List<ActiveItem>();
        foreach(var i in Instance.GetList())
        {
            if(i is ActiveItem p) result.Add(p);
        }
        return result;
    }
    public override ushort ConvertId(string id)
    {
        return ItemDB.Instance.ConvertId(id);
    }

    public override string ConvertId(ushort id)
    {
        return ItemDB.Instance.ConvertId(id);
    }

    public override Item Create(string id, bool isIdCounting)
    {
        var data = ItemDB.Instance.GetData(id);
        var item = data.CreateItemInstance();
        item.Init(data);
        item.ObjId = id;
        if(isIdCounting)
        {
            item.IndexId = GetNextId();
        }
        Items.Add(item);
        return item;
    }
    public override void GameUpdate()
    {
        if(GameManager.Instance.CurPlayer.IsDead) return;
        base.GameUpdate();
    }
    public override void LateGameUpdate()
    {
        if(GameManager.Instance.CurPlayer.IsDead) return;
        base.LateGameUpdate();
    }
}