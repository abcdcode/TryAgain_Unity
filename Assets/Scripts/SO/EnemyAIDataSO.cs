using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine;

public abstract class EnemyAIDataSO : ReplaySO<Enemy>
{
    [SerializeField]protected bool leftOut = true;
    public virtual void Init(Enemy enemy)
    {
    }
    public override void GameUpdate(Enemy obj)
    {
        base.GameUpdate(obj);
        if(leftOut)
        {
            if(obj.Position.x < -(960+obj.GetSize().x+50))
            {
                obj.Delete();
            }
        }
    }
    public virtual EnemyAIState BuildAIState(Enemy enemy)
    {
        return new EnemyAIState();
    }
}