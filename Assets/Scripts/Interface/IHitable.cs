public interface IHitable : IDamageable
{
    public float HitSize{get;}
    public ReplayMono Obj
    {
        get
        {
            if(this is ReplayMono r) return r;
            return null;
        }
    }
}