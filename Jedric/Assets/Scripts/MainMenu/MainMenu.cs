using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGameButton()
    {
        // Load new play scene
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadGameButton()
    {
        //Load previous play scene
        DataPersistenceManager.instance.LoadGame();
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
