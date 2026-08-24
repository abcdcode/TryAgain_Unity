using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ReplayObjContainer<T> : SingletonBehavior<ReplayObjContainer<T>>, IReplayable where T : IReplayObj
{
    public virtual void Save(SaveData data)
    {
        data.Write(Items.Count);
        foreach(var i in Items)
        {
            data.Write(i.IndexId);
            data.Write(ConvertId(i.ObjId));
            i.Save(data);
        }
    }
    public virtual void Load(SaveData data)
    {
        int count = data;
        List<int> list = new ();
        for(int i = 0 ; i < count; i++)
        {
            int indexId = data;
            int objId = data;
            var item = Items.Find(x => x.IndexId == indexId);
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
        foreach(var i in Items.ToArray())
        {
            if(!list.Contains(i.IndexId))
            {
                i.Delete();
            }
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
    public abstract int ConvertId(string id);
    public abstract string ConvertId(int id);
    public abstract T Create(string id, bool isIdCounting);
    public virtual void GameUpdate()
    {
        GetList().ForEach(x => x.GameUpdate());
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