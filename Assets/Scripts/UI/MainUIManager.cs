using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : SingletonBehavior<MainUIManager>
{
    public void Awake()
    {
        m_GameOverUI.SetActive(false);
        m_GameClearUI.SetActive(false);
        m_ESCManu.SetActive(false);
    }
    public void OpenGameOver()
    {
        m_GameOverUI.SetActive(true);
    }
    public void OpenGameClear()
    {
        m_GameClearUI.SetActive(true);
    }
    public void ClickGoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void SetESCMenu(bool isOpen)
    {
        m_ESCManu.SetActive(isOpen);
    }
    public void Update()
    {
        var cur = StageManager.Instance.CurWaveNum;
        var max = StageManager.LastWave;
        m_WaveText.text = $"{cur+1}/{max} Waves";
        if(m_bossid == -1)
        {
            m_BossHpBar.gameObject.SetActive(false);
        }
        else
        {
            var e = EnemyContainer.Instance.GetList().Find(x => x.IndexId == m_bossid);
            if(e == null)
            {
                m_bossid = -1;
                m_BossHpBar.gameObject.SetActive(false);
            }
            else
            {
                m_BossHpBar.gameObject.SetActive(true);
                m_BossHpBar.fillAmount = e.HP / e.EnemyData.Hp;
            }
        }
    }
    public void SetBoss(int id)
    {
        m_bossid = id;
    }
    [SerializeField]private int m_bossid;
    [SerializeField]private Image m_BossHpBar;
    [SerializeField]private GameObject m_ESCManu;
    [SerializeField]private GameObject m_GameOverUI;
    [SerializeField]private GameObject m_GameClearUI;
    [SerializeField]private TextMeshProUGUI m_WaveText;
}