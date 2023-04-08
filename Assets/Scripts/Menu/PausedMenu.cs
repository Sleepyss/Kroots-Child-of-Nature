using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;

    public GameObject waypoint;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GamePaused){
                Resume();
            }else {
                Pause();
            }
        }
    }

    public void NextLevel(){
        SceneManager.LoadScene("Level 2 (Boss Fight)");
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Menu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit(){
        Application.Quit();
    }
}
