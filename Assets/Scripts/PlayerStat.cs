using System;
using System.Collections.Generic;

public class PlayerStat
{
    private const float BaseSpeed = 1000;
    private T StatModifying<T>(T def, List<Item> list, Func<T,Item,T> func)
    {
        foreach(var l in list)
        {
            def = func(def,l);
        }
        return def;
    }
    public List<Item> Inven => Inventory.Instance.GetList();
    public float MoveSpeed => BaseSpeed * StatModifying(1f,Inven,(t,l) => t * l.MoveSpeedMult());
    public float AllDamage => 1 * StatModifying(1f,Inven,(t,l) => t * l.AllDamageMult());
}