using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.ColorUtility;

public class CheckPointBehaviour : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject[] checkPoints;
    public GameObject stats;

    public int SceneIndex;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Finish");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level != SceneIndex)
        {
            Destroy(gameObject);
        }
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
                WinScreen.SetActive(true);
                collision.gameObject.SetActive(false);
                stats.GetComponent<StatisticManager>().OnGameFinish();
            } else {
                LoseScreen.SetActive(true);
                collision.gameObject.SetActive(false);
                stats.GetComponent<StatisticManager>().OnGameFinish();
            }
        }
    }
}
