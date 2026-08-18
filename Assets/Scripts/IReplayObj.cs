public interface IReplayObj : IReplayable
{
    public void Delete();
    public int IndexId{get;set;}
    public string ObjId{get;set;}
}