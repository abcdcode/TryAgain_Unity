using System;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(DamageInfo dmg);
    public FactionEnum Faction{get;}

}
[Serializable]
public class DamageInfo : IReplayable
{
    [SerializeField]private float m_dmg;
    public float dmg{
        get
        {
            return m_dmg;
        }
        set
        {
            m_dmg = value;
        }
    }
    [SerializeField]private FactionEnum m_faction;
    public FactionEnum faction{
        get
        {
            return m_faction;
        }
        set
        {
            m_faction = value;
        }
    }

    public void GameUpdate()
    {
    }

    public void LateGameUpdate()
    {
    }

    public void Load(SaveData data)
    {
        dmg = data;
        faction = (FactionEnum)(byte)data;
    }

    public void Save(SaveData data)
    {
        data.Write(dmg);
        data.Write((byte)faction);
    }
}