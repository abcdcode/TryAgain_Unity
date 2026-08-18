public class Bullet : ReplayMono
{
    public void Init(BulletSO d)
    {
        m_Data = d;
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        m_Data.GameUpdate(this);
    }
    public override void Delete()
    {
        BulletContainer.Instance.Delete(this);
    }
    public override void ExecuteCool(int id)
    {
        BulletCoolEnum e = (BulletCoolEnum)id;
        if(e == BulletCoolEnum.Destroy)
        {
            Delete();
            return;
        }
    }
    public BulletSO m_Data;
}
public enum BulletCoolEnum
{
    Destroy
}