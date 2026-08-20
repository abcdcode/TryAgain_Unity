using UnityEngine;

public abstract class BulletSO : ReplaySO<Bullet>
{
    [SerializeField]public SpriteSO m_spriteSO;
    [SerializeField]public float m_bulletSize;
    public virtual void Init(Bullet bullet)
    {
        
    }
    public override void Save(SaveData data, Bullet bullet)
    {
        
    }
    public override void Load(SaveData data, Bullet bullet)
    {
        
    }
    public override void GameUpdate(Bullet bullet)
    {
        
    }
    
}