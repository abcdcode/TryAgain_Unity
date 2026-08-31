using UnityEngine.SceneManagement;

public class TitleManager : SingletonBehavior<TitleManager>
{
    public void ClickGameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
}