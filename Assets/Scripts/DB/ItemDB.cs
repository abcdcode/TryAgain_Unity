using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class ItemDB : DataDB<ItemDataSO>
{
    public static List<ItemDataSO> GetReward(int count, ItemGrade grade)
    {
        var list = Instance.GetList().FindAll(x => x.Grade == grade);
        list.RemoveAll(x => Inventory.Instance.GetList().Find(y => y.Data == x) != null);
        List<ItemDataSO> result = new List<ItemDataSO>();
        for(int i = 0 ; i < count; i++)
        {
            if(list.Count == 0) return result;
            int n = SeedManager.Instance.GetInt(0,list.Count);
            result.Add(list[n]);
            list.RemoveAt(n);
        }
        return result;
    }
}