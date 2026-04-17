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

    public bool IsWon;

    private void OnTriggerEnter(Collider other)
    {
        IsWon = true;

        WinnerPanel.SetActive(true);
        PlayerUI.SetActive(false);

        Time.timeScale = 0f;

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

    public bool IsTheWinner()
    {
        Debug.Log($"Returning {IsWon} as IsWon");
        return IsWon;
    }
}