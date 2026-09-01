using UnityEngine;
public class DefaultCoolActiveItemDataSO : ActiveItemDataSO
{
    [SerializeField]protected float m_cool;
    public float Cool => m_cool;
}