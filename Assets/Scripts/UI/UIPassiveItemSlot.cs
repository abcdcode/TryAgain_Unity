using UnityEngine;
using UnityEngine.UI;

public class UIPassiveItemSlot : GameMono
{
    public void Init(Item item)
    {
        m_item = item;
        m_image.sprite = item.ItemSprite();
    }
    [SerializeField]private Image m_image;
    private Item m_item;
}