using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataDB<T> : SingletonBehavior<DataDB<T>> where T : SOData
{
    public override void Awake()
    {
        base.Awake();
        idDic = new BiDictionary<string, ushort>();
        itemDic = new Dictionary<ushort, T>();
        ushort id = 1;
        foreach(var i in items)
        {
            idDic.Add(i.m_Id,id);
            itemDic.Add(id,i);
            id++;
        }
    }
    public virtual ushort GetId(T data)
    {
        return ConvertId(data.m_Id);
    }
    public virtual T GetData(string id)
    {
        return GetData(idDic.Get(id));
    }
    public virtual T GetData(ushort id)
    {
        if(!itemDic.ContainsKey(id)) return null;
        return itemDic[id];
    }
    public virtual string ConvertId(ushort id)
    {
        return idDic.Get(id);
    }
    public virtual ushort ConvertId(string id)
    {
        return idDic.Get(id);
    }
    protected BiDictionary<string,ushort> idDic;
    protected Dictionary<ushort, T> itemDic;
    [SerializeField] protected List<T> items;
}