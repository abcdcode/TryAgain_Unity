using UnityEngine;
public class EnemyDataSO : ReplaySO<Enemy>
{
    [SerializeField]public float Hp;
    [SerializeField]public GameObject m_Prefab;
    [SerializeField]public float m_Size;
    public virtual void Init(Enemy obj)
    {
        obj.SetSize(new Vector2(m_Size,m_Size));
    }
}