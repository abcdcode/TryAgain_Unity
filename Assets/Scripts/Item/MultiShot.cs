
using UnityEngine;

public class MultiShot : CoolActiveItem
{
    public override void Init(ItemDataSO data)
    {
        base.Init(data);
        m_shootTimer = 0;
        m_remainShoot = 0;
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(m_shootTimer);
        data.Write(m_remainShoot);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        m_shootTimer = data;
        m_remainShoot = data;
    }
    public override void Active()
    {
        base.Active();
        m_remainShoot = 15;
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        m_shootTimer -= Time.deltaTime;
        if(m_shootTimer <= 0)
        {
            m_shootTimer = 0.1f;
            if(m_remainShoot > 0)
            {
                var p = GameManager.Instance.CurPlayer;
                var b = p.CreateMainBullet();
                b.InitPos(p.Position);
                b.Angle = SeedManager.Instance.GetFloat(-10,10);
                b.SetSize(new Vector2(60, 60));
                b.damageInfo = new DamageInfo() { dmg = p.Stat.GetMainDmg(), faction = FactionEnum.Player };
                m_remainShoot -= 1;
            }
        }
    }

    private float m_shootTimer;
    private int m_remainShoot;
}