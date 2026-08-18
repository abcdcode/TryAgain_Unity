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
        CurPlayer = Instantiate(m_playerPrefab).GetComponent<Player>();
        CurPlayer.Position = new Vector2(0,0);
    }

    void Update()
    {
        CurPlayer.GameUpdate();
        foreach(var c in m_ContainerList)
        {
            c.GameUpdate();
        }
    }
    public Player CurPlayer{get;private set;}
    private List<IReplayable> m_ContainerList;
    [SerializeField]private GameObject m_playerPrefab;
    [SerializeField]private List<MonoBehaviour> Containers;
}
