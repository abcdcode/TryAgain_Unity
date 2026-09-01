using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIRewardList : SingletonBehavior<UIRewardList>
{
    public override void Awake()
    {
        base.Awake();
        CreateSlot();
        CreateSlot();
        CreateSlot();
        CloseSlots();
    }
    public void CreateSlot()
    {
        var slot = Instantiate(m_prefab).GetComponent<UIRewardSlot>();
        slot.transform.SetParent(this.transform);
        slot.Scale = new Vector2(1,1);
        slots.Add(slot);
    }
    public void CloseSlots()
    {
        foreach(var s in slots)
        {
            s.gameObject.SetActive(false);
        }
    }
    public void SetReward(List<ItemDataSO> data)
    {
        CloseSlots();
        for(int i = 0 ; i < data.Count; i++)
        {
            var d = data[i];
            if(slots.Count <= i) CreateSlot();
            slots[i].gameObject.SetActive(true);
            slots[i].Init(d);
        }
    }
    public void SelectSlot(ItemDataSO item)
    {
        CloseSlots();
        Inventory.Instance.Create(item.m_Id,true);
    }
    [SerializeField]private GameObject m_prefab;
    [SerializeField]private List<UIRewardSlot> slots;
}