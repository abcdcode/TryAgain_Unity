using UnityEngine;

public abstract class EffectDataSO : ReplaySO<Effect>
{
    [SerializeField]private GameObject m_Prefab;
    public virtual Effect BuildEffect()
    {
        var effect = Instantiate(m_Prefab).GetComponent<Effect>();
        effect.Init(this);
        return effect;
    }
}