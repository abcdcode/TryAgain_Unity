using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleManager : SingletonBehavior<TitleManager>
{
    public void Start()
    {
        if(GlobalManager.Instance.IsSaveExist())
        {
            m_ContinueBtn.SetActive(true);
        }
        else
        {
            m_ContinueBtn.SetActive(false);
        }
    }
    public void ClickGameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ClickContinue()
    {
        SceneManager.LoadScene("MainScene");
        //ReplayHamburger.Instance.LoadInSaveFile();
        GameManager.IsContinue = true;
    }
    [SerializeField]private GameObject m_ContinueBtn;
}