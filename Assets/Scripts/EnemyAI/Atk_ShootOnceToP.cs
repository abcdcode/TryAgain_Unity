using UnityEngine;

public class Atk_ShootOnceToP : AttackInfo
{
    public override void Execute(Enemy e)
    {
        var b = BulletContainer.Instance.Create(BulletDB.EnemyTestBullet, true);
            b.InitPos(e.Position);
            b.LookAt(GameManager.Instance.CurPlayer.Position);
            b.damageInfo = new DamageInfo(){dmg = 1, faction = FactionEnum.Enemy};
            b.SetSize(new Vector2(30, 30));
    }
}