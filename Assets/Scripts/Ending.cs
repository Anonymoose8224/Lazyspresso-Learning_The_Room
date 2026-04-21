using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] TempPauseMenu pause;
    [SerializeField] private GameObject WinnerPanel;
    [SerializeField] private GameObject PlayerUI;

    public bool IsWon = false;

    private void OnTriggerEnter(Collider other)
    {
        WinnerPanel.SetActive(true);
        PlayerUI.SetActive(false);

        Time.timeScale = 0f;
        IsWon = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void WinnerQuitToMainMenu()
    {
        pause.QuitToMainMenu();
    }

    public void Restart()
    {
        pause.Restart();
    }

    public void SwitchScene()
    {
        pause.EndingScene();
    }

    public bool IsTheWinner()
    {
        return IsWon;
    }
}