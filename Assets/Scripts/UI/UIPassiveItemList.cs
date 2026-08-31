using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPassiveItemList : SingletonBehavior<UIPassiveItemList>
{
    public override void Awake()
    {
        base.Awake();
        m_activeSlots = new List<UIPassiveItemSlot>();
        slotPool = new Queue<UIPassiveItemSlot>();
    }
    public void Update()
    {
        foreach(var s in m_activeSlots.ToList())
        {
            DeleteSlot(s);
        }
        foreach(var i in Inventory.GetPassives())
        {
            CreateSlot(i);
        }
    }
    public void CreateSlot(Item item)
    {
        UIPassiveItemSlot slot;
        if(slotPool.Count > 0)
        {
            slot = slotPool.Dequeue();
            slot.gameObject.SetActive(true);
            slot.Init(item);
        }
        else
        {
            slot = Instantiate(prefab).GetComponent<UIPassiveItemSlot>();
            slot.transform.SetParent(this.transform);
            slot.Scale = new Vector2(1,1);
            slot.Init(item);
        }
        m_activeSlots.Add(slot);
    }
    public void DeleteSlot(UIPassiveItemSlot slot)
    {
        slot.gameObject.SetActive(false);
        m_activeSlots.Remove(slot);
        slotPool.Enqueue(slot);
    }
    private List<UIPassiveItemSlot> m_activeSlots;
    private Queue<UIPassiveItemSlot> slotPool;
    [SerializeField]private GameObject prefab;
}