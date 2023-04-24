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
    public GameObject InGameCanvas;
    public GameObject LevelSelectCanvas;
    public GameObject SettingCanvas;
    public GameObject PauseCanvas;
    public GameObject WinCanvas;
    public GameObject HintCanvas;
    public GameObject ResetButton;
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
        InGameCanvas.SetActive(false);
        LevelSelectCanvas.SetActive(false);
        SettingCanvas.SetActive(false);
    }

    public void BackToMainMenuFromGame(){
        SceneManager.LoadScene("AMainMenu");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void InGame(){
        Time.timeScale = 1f;
        // Buttons
        //PauseButton.SetActive(true);
        //PauseMenuButtons.SetActive(false);
        // Canvas
        MainMenuCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
        HintCanvas.SetActive(false);
        // PauseCanvas.SetActive(false);
        WinCanvas.SetActive(false);
    }
    public void ShowHint(){
        HintCanvas.SetActive(HintCanvas.activeSelf ? false: true);
    }

    public void Restart(){
        Time.timeScale = 1f;
        ResetButton.GetComponent<RESET>().Reset();
        
        // PauseButton.SetActive(true);
        // PauseCanvas.SetActive(false);
        // PauseMenuButtons.SetActive(false);

    }

    public void Next(){
        Time.timeScale = 1f;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentIndex +1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentIndex + 1);
            }
    }

    public void PauseMenu(){
        Time.timeScale = 0f;
        PauseCanvas.SetActive(true);
        PauseButton.SetActive(false);
        PauseMenuButtons.SetActive(true);
    }

    public void ShowWinMenu(){
        WinCanvas.SetActive(true);
        PauseButton.SetActive(false);
        PauseCanvas.SetActive(false);
            
    }


    // Load levels

    public void LoadScene1(){
        SceneManager.LoadScene(1);
    }

    public void LoadScene2(){
        SceneManager.LoadScene(2);
    }

    public void LoadScene3(){
        SceneManager.LoadScene(3);
    }

    public void LoadScene4(){
        SceneManager.LoadScene(4);
    }

    public void LoadScene5(){
        SceneManager.LoadScene(5);
    }

    public void LoadScene6(){
        SceneManager.LoadScene(6);
    }

    public void LoadScenn7(){
        SceneManager.LoadScene(7);
    }

    public void LoadScene8(){
        SceneManager.LoadScene(8);
    }

    public void LoadScene9(){
        SceneManager.LoadScene(9);
    }

    public void LoadScene10(){
        SceneManager.LoadScene(10);
    }

    public void LoadScene11(){
        SceneManager.LoadScene(11);
    }

    public void LoadScene12(){
        SceneManager.LoadScene(12);
    }

    public void LoadScene13(){
        SceneManager.LoadScene(13);
    }

    public void LoadScene14(){
        SceneManager.LoadScene(14);
    }
    
    public void LoadScene15(){
        SceneManager.LoadScene(15);
    }

}
