using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public abstract class ItemDataSO : ReplaySO<Item>
{
    [SerializeField]
    protected string script;
    [SerializeField]protected List<GameObject> m_Prefab;
    [SerializeField]protected ItemGrade m_grade;
    [SerializeField]protected string m_ItemName;
    [SerializeField]protected string m_ItemDesc;
    public ItemGrade Grade => m_grade;
    public List<GameObject> Prefab => m_Prefab;
    public string ItemName => m_ItemName;
    public string ItemDesc => m_ItemDesc;
    public virtual Item CreateItemInstance()
    {
        var t = Type.GetType(script);
        return (Item)Activator.CreateInstance(t);
    }
}
public enum ItemGrade
{
    Normal,
    Boss
}