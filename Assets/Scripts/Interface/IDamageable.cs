public interface IDamageable
{
    public void TakeDamage(DamageInfo dmg);
    public FactionEnum Faction{get;}

}
public class DamageInfo : IReplayable
{
    private float m_dmg;
    public float dmg{
        get
        {
            return m_dmg;
        }
        set
        {
            if(m_dmg == value) return;
            m_dmg = value;
            m_IsSave = true;
        }
    }
    private FactionEnum m_faction;
    public FactionEnum faction{
        get
        {
            return m_faction;
        }
        set
        {
            if(m_faction == value) return;
            m_faction = value;
            m_IsSave = true;
        }
    }
    private bool m_IsSave = true;

    public void GameUpdate()
    {
    }

    public void LateGameUpdate()
    {
    }

    public void Load(SaveData data)
    {
        m_IsSave = data;
        if(!m_IsSave) return;
        dmg = data;
        faction = (FactionEnum)(byte)data;
    }

    public void Save(SaveData data)
    {
        data.Write(m_IsSave);
        if(!m_IsSave) return;
        data.Write(dmg);
        data.Write((byte)faction);
        m_IsSave = false;
    }
}