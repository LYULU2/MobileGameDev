using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.ColorUtility;
using UnityEngine.SceneManagement;

public class CheckPointBehaviour : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject[] checkPoints;
    public GameObject Stats;

    public int SceneIndex;
    /*
        private void Awake()
        {
            Debug.Log("Awake");
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Finish");
            Debug.Log(objs.Length > 1 && SceneIndex != SceneManager.GetActiveScene().buildIndex);
            if (objs.Length > 1 && SceneIndex != SceneManager.GetActiveScene().buildIndex)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnLevelWasLoaded(int level)
        {
            Debug.Log("LevelWasLoaded");
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Finish");
            if (level != SceneIndex)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(objs[1]);
            }
        }
    */
    private void Reset()
    {
        
    }
    private void Start() {
        checkPoints = GameObject.FindGameObjectsWithTag("Finish");
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Color playerColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            
            Color wallColor = gameObject.GetComponent<SpriteRenderer>().color;
            string playerColorHex = ColorUtility.ToHtmlStringRGBA(playerColor);
            string wallColorHex = ColorUtility.ToHtmlStringRGBA(wallColor);
            if (playerColorHex == wallColorHex) {
                Stats.GetComponent<StatisticManager>().OnGameFinish();
                WinScreen.SetActive(true);
                collision.gameObject.SetActive(false);
                //post data if win the tutorial
                if (SceneManager.GetActiveScene().name == "Scene1")
                {
                    Stats.GetComponent<StatisticManager>().OnGameFinishTutorial(1);
                }
            } else {
                
                /*
                Stats.GetComponent<StatisticManager>().OnGameFinish();
                LoseScreen.SetActive(true);
                collision.gameObject.SetActive(false);
                */
            }
        }
    }
}
