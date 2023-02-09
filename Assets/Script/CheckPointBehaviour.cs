using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ColorUtility;

public class CheckPointBehaviour : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject[] checkPoints;
    public GameObject stats;
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
            //playerColor.r = Mathf.Round(playerColor.r * 10) / 10;
            //playerColor.b = Mathf.Round(playerColor.b * 10) / 10;
            //playerColor.g = Mathf.Round(playerColor.g * 10) / 10;
            // Debug.Log(playerColor.r + "" + playerColor.b );
            //wallColor.r = Mathf.Round(wallColor.r * 10) / 10;
            //wallColor.b = Mathf.Round(wallColor.b * 10) / 10;
            //wallColor.g = Mathf.Round(wallColor.g * 10) / 10;
            // Debug.Log(wallColor);
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
