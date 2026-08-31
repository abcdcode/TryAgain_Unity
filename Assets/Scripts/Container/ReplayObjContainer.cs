using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public abstract class ReplayObjContainer<T> : SingletonBehavior<ReplayObjContainer<T>>, IReplayable where T : IReplayObj
{
    public override void Awake()
    {
        base.Awake();
        Clear();
    }
    public virtual void Save(SaveData data)
    {
        data.Write(Items.Count);
        Items.Sort(CompareItem);
        for (int i = 0; i < Items.Count; i++)
        {
            var it = Items[i];
            data.Write(it.IndexId);
            data.Write(ConvertId(it.ObjId));
            it.Save(data);
        }
    }
    public int CompareItem(T x, T y)
    {
        if (x.IndexId > y.IndexId)
        {
            return 1;
        }
        return -1;
    }

    /*
    public virtual void Save(SaveData data)
    {
        data.Write(Items.Count);
        for(int i=0 ; i < Items.Count; i++)
        {
            var it = Items[i];
            data.Write(it.IndexId);
            data.Write(it.ObjId);
            it.Save(data);
        }
    }
    */
    public virtual void Load(SaveData data)
    {
        int count = data;
        List<T> list = GetList();
        int arrayIndex = 0;
        for (int i = 0; i < count; i++)
        {
            int indexId = data;
            ushort objId = data;
            bool isLoad = false;
            while (arrayIndex < Items.Count)
            {
                var it = Items[arrayIndex];
                var itIndex = it.IndexId;
                if (itIndex < indexId)
                {
                    arrayIndex += 1;
                    continue;
                }
                if (itIndex == indexId)
                {
                    arrayIndex += 1;
                    it.Load(data);
                    isLoad = true;
                    list.Remove(it);
                    break;
                }
                if (itIndex > indexId)
                {
                    break;
                }
            }
            if (isLoad)
            {
                continue;
            }
            var item = Create(ConvertId(objId), false);
            item.IndexId = indexId;
            item.ObjId = ConvertId(objId);
            item.Load(data);
        }
        foreach (var i in list)
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
        if (t is ReplayMono mono)
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
    public virtual void Clear()
    {
        foreach (var i in GetList())
        {
            i.Delete();
        }
        Items.Clear();
    }
    [SerializeField] protected List<T> Items = new List<T>();
}