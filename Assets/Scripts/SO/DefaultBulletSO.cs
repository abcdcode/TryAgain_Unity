using UnityEngine;
public class DefaultBulletSO : BulletSO
{
    [SerializeField]protected float m_time;
    [SerializeField]protected float m_speed;
    public override void Init(Bullet bullet)
    {
        base.Init(bullet);
        bullet.m_CoolTimer.SetCool((int)BulletCoolEnum.Destroy,m_time,0,true);
    }
    public override void GameUpdate(Bullet bullet)
    {
        base.GameUpdate(bullet);
        bullet.Position += (Vector2)bullet.transform.right.normalized * m_speed * Time.deltaTime;
    }
}