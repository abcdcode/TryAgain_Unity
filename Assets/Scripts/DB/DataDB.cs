using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataDB<T> : SingletonBehavior<DataDB<T>> where T : SOData
{
    public override void Awake()
    {
        base.Awake();
        idDic = new BiDictionary<string, int>();
        itemDic = new Dictionary<int, T>();
        int id = 1;
        foreach(var i in items)
        {
            idDic.Add(i.m_Id,id);
            itemDic.Add(id,i);
            id++;
        }
    }
    public virtual int GetId(T data)
    {
        foreach(var pair in itemDic)
        {
            if(pair.Value == data)
            {
                return pair.Key;
            }
        }
        return -1;
    }
    public virtual T GetData(string id)
    {
        return GetData(idDic.Get(id));
    }
    public virtual T GetData(int id)
    {
        if(!itemDic.ContainsKey(id)) return null;
        return itemDic[id];
    }
    public virtual string ConvertId(int id)
    {
        return idDic.Get(id);
    }
    public virtual int ConvertId(string id)
    {
        return idDic.Get(id);
    }
    protected BiDictionary<string,int> idDic;
    protected Dictionary<int, T> itemDic;
    [SerializeField] protected List<T> items;
}