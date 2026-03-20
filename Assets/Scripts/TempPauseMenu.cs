using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject PlayerUI;

    private bool isPaused = false;
    public bool isAble = false;

    private void Awake()
    {
        if (isAble)
            return;
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
            pausePanel.SetActive(true);
         PlayerUI.SetActive(false);


        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
            pausePanel.SetActive(false);
        PlayerUI.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitToMainMenu()
    {
        Debug.Log("Quiting to main menu!");
        SceneManager.LoadScene("Main Menu");
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}