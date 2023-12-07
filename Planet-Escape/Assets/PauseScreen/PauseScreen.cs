using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseScreen;
    private bool isPauseScreenOpen;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPauseScreenOpen = !isPauseScreenOpen;
            pauseScreen.SetActive(isPauseScreenOpen);
        }
    }

    public void Resume()
    {
        isPauseScreenOpen = !isPauseScreenOpen;
        pauseScreen.SetActive(isPauseScreenOpen);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
