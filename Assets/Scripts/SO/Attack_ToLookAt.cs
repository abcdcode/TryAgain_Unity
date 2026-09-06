public class Attack_ToLookAt : AttackDataSO
{
    public override void Shoot(Enemy e)
    {
        AttackDataSO.ShootEnemyDefaultBullet(e.Position,e.Angle);
    }
}