using UnityEngine;

[RequireComponent(typeof(ReplayMono))]
public class ReplayAnimator :GameMono,IReplayable
{
    public void Awake()
    {
        m_parent = GetComponent<ReplayMono>();
    }
    public void GameUpdate()
    {
    }

    public void Load(SaveData data)
    {
    }

    public void Save(SaveData data)
    {
    }
    protected ReplayMono m_parent;
}