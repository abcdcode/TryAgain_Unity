using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public abstract class EnemyAIDataSO : ReplaySO<Enemy>
{
    public virtual void Init(Enemy enemy)
    {
    }
}