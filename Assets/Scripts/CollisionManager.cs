using System.Collections.Generic;
using System.Linq;

public class CollisionManager : SingletonBehavior<CollisionManager>
{
    public override void Awake()
    {
        base.Awake();
        m_ColList = new List<ICollision>();
    }
    public void Update()
    {
        foreach(var c1 in List)
        {
            
        }
    }
    public void RegisterCol(ICollision col)
    {
        m_ColList.Add(col);
    }
    public void UnRegisterCol(ICollision col)
    {
        m_ColList.Remove(col);
    }
    public List<ICollision> List => m_ColList.ToList();
    private List<ICollision> m_ColList;
}