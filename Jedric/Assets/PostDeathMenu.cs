using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostDeathMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PostDeathMenuUI;

    public void RetryLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitLevel() {
        Debug.Log("Go to main room");
    }
}
