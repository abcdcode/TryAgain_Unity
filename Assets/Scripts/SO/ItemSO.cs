using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ReplaySO<Item>
{
    protected List<GameObject> m_Prefab;
    public List<GameObject> Prefab => m_Prefab;
}