using System;
using System.Collections.Generic;

public class PlayerStat
{
    private const float BaseSpeed = 1000;
    private const float AtkSpeed = 0.4f;
    private const float DefaultDmg = 10;
    public float GetMoveSpeed()
    {
        var result = BaseSpeed;
        foreach(var i in Inven)
        {
            result *= i.MoveSpeedMult();
        }
        return result;
    }
    public float GetAtkSpeed()
    {
        var result = AtkSpeed;
        foreach(var i in Inven)
        {
            result *= i.AtkSpeedMult();
        }
        return result;
    }
    public float GetMainDmg()
    {
        var result = DefaultDmg;
        foreach(var i in Inven)
        {
            result *= i.AllDamageMult();
            result *= i.MainDamageMult();
        }
        return result;
    }
    public List<Item> Inven => Inventory.Instance.GetList();
}