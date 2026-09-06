using UnityEngine;

public abstract class AttackDataSO : SOData
{
    public static Bullet ShootEnemyDefaultBullet(Vector2 pos, float angle)
    {
        var b = BulletContainer.Instance.Create(BulletDB.EnemyTestBullet, true);
        b.InitPos(pos);
        b.Angle = angle;
        b.damageInfo = new DamageInfo() { dmg = 1, faction = FactionEnum.Enemy };
        b.SetSize(new Vector2(30, 30));
        return b;
    }
    public abstract void Shoot(Enemy e);
}