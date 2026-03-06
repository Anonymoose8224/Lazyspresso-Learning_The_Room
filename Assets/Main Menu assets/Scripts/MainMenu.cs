using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // name of your game scene
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed"); // only shows in editor
    }
}
