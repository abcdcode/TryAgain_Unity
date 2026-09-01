using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : SingletonBehavior<MainUIManager>
{
    public void Awake()
    {
        m_GameOverUI.SetActive(false);
    }
    public void OpenGameOver()
    {
        m_GameOverUI.SetActive(true);
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
    }
    [SerializeField]private GameObject m_ESCManu;
    [SerializeField]private GameObject m_GameOverUI;
    [SerializeField]private TextMeshProUGUI m_WaveText;
}