public class Bullet : ReplayMono
{
    public override void Delete()
    {
        BulletContainer.Instance.Delete(this);
    }
}