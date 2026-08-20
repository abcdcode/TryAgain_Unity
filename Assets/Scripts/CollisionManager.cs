using System.Collections.Generic;

public class CollisionManager : SingletonBehavior<CollisionManager>
{
    public override void Awake()
    {
        base.Awake();
        m_ColList = new List<ICollision>();
    }
    public void Update()
    {
        
    }
    public void RegisterCol(ICollision col)
    {
        m_ColList.Add(col);
    }
    public void UnRegisterCol(ICollision col)
    {
        m_ColList.Remove(col);
    }
    private List<ICollision> m_ColList;
}