using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioPlayer  MusCubeMenu;

    private void Awake()
    {
        MusCubeMenu.PlayMusic();
    }
    public void StartGame()
    {
        MusCubeMenu.PauseMusic();
        SceneManager.LoadScene("GameScene"); // name of your game scene
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed"); // only shows in editor
    }
}
