public interface IStatOwner
{
    public CharacterStat Stat{get;}
    public GameMono Obj => (GameMono)this;
}