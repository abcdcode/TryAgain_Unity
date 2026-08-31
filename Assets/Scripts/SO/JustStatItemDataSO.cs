using UnityEngine;
public class JustStatItemDataSO : PassiveItemDataSO
{
    [SerializeField]protected float m_allDmgMult = 1;
    [SerializeField]protected float m_mainDmgMult = 1;
    [SerializeField]protected float m_subDmgMult = 1;
    [SerializeField]protected float m_pMoveMult = 1;
    [SerializeField]protected float m_atkSpeed = 1;
    public float AllDmgMult => m_allDmgMult;
    public float MainDmgMult => m_mainDmgMult;
    public float SubDmgMult => m_subDmgMult;
    public float MoveMult => m_pMoveMult;
    public float AtkSpeed => m_atkSpeed;
}