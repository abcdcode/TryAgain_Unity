public class EffectContainer : ReplayObjContainer<Effect>
{
    public override ushort ConvertId(string id)
    {
        return EffectDB.Instance.ConvertId(id);
    }

    public override string ConvertId(ushort id)
    {
        return EffectDB.Instance.ConvertId(id);
    }

    public override Effect Create(string id, bool isIdCounting)
    {
        var d = EffectDB.Instance.GetData(id);
        var e = d.BuildEffect();
        e.ObjId = id;
        if(isIdCounting)
        {
            e.IndexId = GetNextId();
        }
        Items.Add(e);
        return e;
    }
}