using System;
using System.Collections.Generic;

public class PlayerStat
{
    private const float BaseSpeed = 1000;
    private const float AtkSpeed = 1;
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

    public List<Item> Inven => Inventory.Instance.GetList();
}