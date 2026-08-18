using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_ContainerList = new List<IReplayable>();
        foreach(var c in Containers)
        {
            if(c is IReplayable i)
            {
                m_ContainerList.Add(i);
            }
        }
    }

    void Update()
    {
        foreach(var c in m_ContainerList)
        {
            c.GameUpdate();
        }
    }
    private List<IReplayable> m_ContainerList;
    [SerializeField]private List<MonoBehaviour> Containers;
}
