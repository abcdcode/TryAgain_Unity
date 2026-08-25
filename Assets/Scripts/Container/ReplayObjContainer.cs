using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ReplayObjContainer<T> : SingletonBehavior<ReplayObjContainer<T>>, IReplayable where T : IReplayObj
{
    public virtual void Save(SaveData data)
    {
        data.Write(Items.Count);
        for(int i=0 ; i < Items.Count; i++)
        {
            var it = Items[i];
            data.Write(it.IndexId);
            data.Write(ConvertId(it.ObjId));
            it.Save(data);
        }
    }
    public virtual void Load(SaveData data)
    {
        int count = data;
        List<int> list = new ();
        for(int i = 0 ; i < count; i++)
        {
            int indexId = data;
            ushort objId = data;
            T item;
            item = Items.Count > i && Items[i].IndexId == indexId ? Items[i] : Items.Find(x => x.IndexId == indexId);
            if(item != null)
            {
                item.Load(data);
            }
            else
            {
                item = Create(ConvertId(objId),false);
                item.IndexId = indexId;
                item.ObjId = ConvertId(objId);
                item.Load(data);
            }
            list.Add(indexId);
        }
        var d = new List<T>();
        foreach(var i in Items)
        {
            if(!list.Contains(i.IndexId))
            {
                d.Add(i);
            }
        }
        foreach(var i in d)
        {
            i.Delete();
        }
    }
    public virtual void Add(T t)
    {
        Items.Add(t);
    }
    public virtual void Delete(T t)
    {
        Items.Remove(t);
            if(t is ReplayMono mono)
            {
                Destroy(mono.gameObject);
            }
        
    }
    public abstract ushort ConvertId(string id);
    public abstract string ConvertId(ushort id);
    public abstract T Create(string id, bool isIdCounting);
    public virtual void GameUpdate()
    {
        GetList().ForEach(x => x.GameUpdate());
    }
    public virtual void LateGameUpdate()
    {
    }
    protected int GetNextId()
    {
        return IdCountManager.Instance.GetNextId();
    }
    public virtual List<T> GetList()
    {
        return Items.ToList();
    }
    [SerializeField]protected List<T> Items = new List<T>();
}