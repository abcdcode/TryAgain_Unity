using UnityEngine;
public class EnemyDataSO : ReplaySO<Enemy>
{
    [SerializeField]protected float Hp;
    public virtual void Init(Enemy obj)
    {
        
    }
}