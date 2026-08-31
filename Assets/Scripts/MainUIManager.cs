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
    [SerializeField]private GameObject m_GameOverUI;
}