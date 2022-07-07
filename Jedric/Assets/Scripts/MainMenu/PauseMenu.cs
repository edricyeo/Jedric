using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("hdsjafh");
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }        
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Retry() {
        Debug.Log("Retry level");
    }

    public void GoToMainRoom() {
        Debug.Log("Go to main room");
    }
}
