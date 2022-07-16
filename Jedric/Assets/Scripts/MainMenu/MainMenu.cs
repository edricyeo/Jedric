using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        //Load the play scene
        SceneManager.LoadScene("MainRoom");
    }

    public void TutorialButton()
    {
        //Load the tutorial scene
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
