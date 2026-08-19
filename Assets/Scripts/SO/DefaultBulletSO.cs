using UnityEngine;
/// <summary>
/// 전방으로 지속시간동안 일정속도로 나아가는 일반 탄환
/// </summary>
public class DefaultBulletSO : BulletSO
{
    [SerializeField]protected float m_time;
    [SerializeField]protected float m_speed;
    public override void Init(Bullet bullet)
    {
        base.Init(bullet);
        //Debug.Log("DefaultBulletSO Init");
        bullet.m_CoolTimer.SetCool((int)BulletCoolEnum.Destroy,m_time,0,true);
    }
    public override void GameUpdate(Bullet bullet)
    {
        base.GameUpdate(bullet);
        bullet.Position += (Vector2)bullet.transform.right.normalized * m_speed * Time.deltaTime;
    }
    public override void ExecuteCool(Bullet bullet, int value)
    {
        base.ExecuteCool(bullet,value);
    }
}