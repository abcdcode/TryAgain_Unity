using UnityEngine;
using UnityEngine.UI;

public class UIActiveItemSlot : MonoBehaviour
{
    public void Update()
    {
        var it = Inventory.GetActives();
        if(it.Count == 0) 
        {
            m_itemSprite.gameObject.SetActive(false);
            return;
        }
        m_itemSprite.gameObject.SetActive(true);
        int i = GameManager.Instance.CurPlayer.CurActiveIndex;
        var item = it[i];
        m_itemSprite.sprite = item.ItemSprite();
        m_coolSprite.fillAmount = item.CoolRemainRate();
    }
    public Image m_itemSprite;
    public Image m_coolSprite;
}