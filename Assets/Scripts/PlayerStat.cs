using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public PlayerStat()
    {
        ReplayGauge = GetMaxReplayGauge();
    }
    private const float BaseSpeed = 1000;
    private const float AtkSpeed = 0.4f;
    private const float DefaultDmg = 10;
    private const float BaseMaxReplayGauge = 1200; // 1초당 120프레임, 게이지 120으로 계산
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
    public Vector2 MainBulletSize()
    {
        float value = 60;
        return new Vector2(value,value);
    }
    public float ReplayGauge
    {
        get
        {
            return m_replayGauge;
        }
        set
        {
            m_replayGauge = value;
            if(m_replayGauge <= 0) m_replayGauge = 0;
        }
    }
    public float GetMaxReplayGauge()
    {
        var result = BaseMaxReplayGauge;
        return result;
    }
    private float m_replayGauge;
    public List<Item> Inven => Inventory.Instance.GetList();
}