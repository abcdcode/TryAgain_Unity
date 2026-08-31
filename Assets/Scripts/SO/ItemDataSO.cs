using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public abstract class ItemDataSO : ReplaySO<Item>
{
    [SerializeField]
    protected MonoScript script;
    [SerializeField]protected List<GameObject> m_Prefab;
    [SerializeField]protected ItemGrade m_grade;
    public ItemGrade Grade => m_grade;
    public List<GameObject> Prefab => m_Prefab;
    public Item CreateItemInstance()
    {
        var t = script.GetClass();
        return (Item)Activator.CreateInstance(t);
    }
}
public enum ItemGrade
{
    Normal,
    Boss
}