using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject SelectLevelButton;
    public GameObject QuitButton;
    public GameObject LevelSelectCanvas;
    // Start is called before the first frame update
    void Start(){
        BackToMain();
    }
    public void GameStart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void SelectLevel(){
        StartButton.SetActive(false);
        QuitButton.SetActive(false);
        SelectLevelButton.SetActive(false);

        LevelSelectCanvas.SetActive(true);
    }

    void Setting(){

    }

    void ShowCredit(){

    }

    public void BackToMain(){
        StartButton.SetActive(true);
        QuitButton.SetActive(true);
        SelectLevelButton.SetActive(true);

        LevelSelectCanvas.SetActive(false);
    }

    public void QuitGame(){
        Application.Quit();
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
