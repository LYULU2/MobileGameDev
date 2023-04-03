using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.ColorUtility;

public class CheckPointBehaviour : MonoBehaviour
{
    public GameObject child;
    public GameObject Stats;
    public AudioSource SoundWrongGoal;

    public int SceneIndex;
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Color playerColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            
            Color wallColor = child.GetComponent<SpriteRenderer>().color;
            string playerColorHex = ColorUtility.ToHtmlStringRGBA(playerColor);
            string wallColorHex = ColorUtility.ToHtmlStringRGBA(wallColor);
            if (playerColorHex == wallColorHex) {
                Stats.GetComponent<StatisticManager>().OnGameFinish();
                GameObject.Find("UI_Manager").GetComponent<UIManager>().ShowWinMenu();
                collision.gameObject.SetActive(false);
            } else {
                
                /*
                Stats.GetComponent<StatisticManager>().OnGameFinish();
                collision.gameObject.SetActive(false);
                */
                SoundWrongGoal.Play();
            }
        }
    }
}
