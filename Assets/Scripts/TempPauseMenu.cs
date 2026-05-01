using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private Ending endingS;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private AudioPlayer AudClip;



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
        AudClip.enabled = false;
        isPaused = false;
        endingS.IsWon = false;
        Time.timeScale = 1f;
        Debug.Log("Quiting to main menu!");
        SceneManager.LoadScene("Main Menu");

    }

    public void Restart()
    {
        isPaused = false;
        endingS.IsWon = false;
        Time.timeScale = 1f;
        inventoryUI.RefreshUI();
        AudClip.PlayMusic();

        Debug.Log("Restarting the Game!");
        SceneManager.LoadScene("GameScene");
    }



    public void EndingScene()
    {
        Debug.Log("GameEnding Closed");
        SceneManager.LoadScene("Pot Ending Scene");

    }

    public bool IsPaused()
    {
        return isPaused;
    }
}