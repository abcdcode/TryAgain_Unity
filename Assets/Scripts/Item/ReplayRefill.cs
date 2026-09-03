public class ReplayRefill : OnceItem
{
    public override void Active()
    {
        base.Active();
        Owner.Stat.ReplayGauge = Owner.Stat.GetMaxReplayGauge();
    }
}