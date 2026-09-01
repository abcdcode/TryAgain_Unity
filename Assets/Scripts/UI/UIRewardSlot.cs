using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIRewardSlot : GameMono
{
    public void Init(ItemDataSO item)
    {
        m_data = item;
        m_image.sprite = m_data.ItemSprite;
        m_title.text = m_data.ItemName;
        m_desc.text = m_data.ItemDesc;
    }
    public void ClickSlot()
    {
        UIRewardList.Instance.SelectSlot(m_data);
    }
    private ItemDataSO m_data;
    [SerializeField]private Image m_image;
    [SerializeField]private TextMeshProUGUI m_title;
    [SerializeField]private TextMeshProUGUI m_desc;
}