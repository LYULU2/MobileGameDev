using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuButtons;
    public GameObject PauseButton;
    public GameObject PauseMenuButtons;
    public GameObject MainMenuCanvas;
    public GameObject LevelSelectCanvas;
    public GameObject SettingCanvas;
    public GameObject PauseCanvas;
    public GameObject WinCanvas;
    public GameObject LoseCanvas;
    // Start is called before the first frame update
    void Start(){
        if (SceneManager.GetActiveScene().buildIndex == 0){
            BackToMain();
        }
        else{
            InGame();
        }
    }
    public void GameStart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void SelectLevel(){
        // Buttons
        MainMenuButtons.SetActive(false);
        // Canvas
        LevelSelectCanvas.SetActive(true);
        SettingCanvas.SetActive(false);
    }

    public void Setting(){
        // Buttons
        MainMenuButtons.SetActive(false);
        // Canvas
        LevelSelectCanvas.SetActive(false);
        SettingCanvas.SetActive(true);
    }

    public void BackToMain(){
        // Buttons
        MainMenuButtons.SetActive(true);
        // Canvas
        MainMenuCanvas.SetActive(true);
        LevelSelectCanvas.SetActive(false);
        SettingCanvas.SetActive(false);
    }

    public void BackToMainMenuFromGame(){
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void InGame(){
        Time.timeScale = 1f;
        // Buttons
        PauseButton.SetActive(true);
        PauseMenuButtons.SetActive(false);
        // Canvas
        MainMenuCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        WinCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseMenu(){
        Time.timeScale = 0f;
        PauseCanvas.SetActive(true);
        PauseButton.SetActive(false);
        PauseMenuButtons.SetActive(true);
    }

    public void LoseMenu(){
        LoseCanvas.SetActive(true);
    }

    public void WinMenu(){
        WinCanvas.SetActive(true);     
    }


    // Load levels

    public void LoadScene1(){
        SceneManager.LoadScene(1);
    }

    public void LoadScene2(){
        SceneManager.LoadScene(2);
    }

    public void LoadScenn3(){
        SceneManager.LoadScene(3);
    }

    public void LoadScene4(){
        SceneManager.LoadScene(4);
    }

}
