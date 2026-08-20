using UnityEngine;
public class EnemyDataSO : ReplaySO<Enemy>
{
    [SerializeField]protected float Hp;
    [SerializeField]public GameObject m_Prefab;
    public virtual void Init(Enemy obj)
    {
        
    }
}