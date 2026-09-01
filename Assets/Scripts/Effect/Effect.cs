public abstract class Effect : ReplayMono
{
    public virtual void Init(EffectDataSO so)
    {
        Data = so;
    }
    public override void Delete()
    {
        EffectContainer.Instance.Delete(this);
    }
    public EffectDataSO Data{get;private set;}
}