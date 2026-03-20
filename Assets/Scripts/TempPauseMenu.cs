using UnityEngine;
using UnityEngine.SceneManagement;

public class TempPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
            Debug.Log("Game is Resuming!");
        }
        else
        {
            PauseGame();
            Debug.Log("Game is pausing!");

        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Game is successfully paused!");
    }

    public void ResumeGame()
    {
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Game is successfully resumed!");

    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Game is loading Main menu!");
    }

    public bool IsPaused()
    {
        //Debug.Log("Game is retriving pause!");
        return isPaused;
    }
}