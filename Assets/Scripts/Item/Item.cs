public abstract class Item : StatModifier, IReplayObj
{
    public virtual void GameUpdate()
    {
    }

    public virtual void LateGameUpdate()
    {
    }

    public virtual void Load(SaveData data)
    {
    }

    public virtual void Save(SaveData data)
    {
    }
    public virtual void Init(ItemDataSO data)
    {
        Data = data;
        Owner = GameManager.Instance.CurPlayer;
    }
    public virtual void OnEquip()
    {
        
    }
    public virtual void OnRelease()
    {
        
    }
    public virtual void Delete()
    {
        OnRelease();
        Inventory.Instance.Delete(this);
    }
    public virtual string ItemName()
    {
        return Data.ItemName;
    }
    public virtual string ItemDesc()
    {
        return Data.ItemDesc;
    }

    public Player Owner{get;private set;}
    public ItemDataSO Data{get;private set;}

    
    public int IndexId { get; set; }
    public string ObjId { get; set; }
}