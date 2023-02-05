using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            if (collision.GetComponent<SpriteRenderer>().color == GameObject.FindGameObjectsWithTag("Finish")[0].GetComponent<SpriteRenderer>().color)
            {
                // size of player must be <= size of check point
                // foreach (GameObject checkPoint in checkPoints) {
                //     if (collision.gameObject.transform.localScale.x <= checkPoint.transform.localScale.x
                //         && collision.gameObject.transform.localScale.y <= checkPoint.transform.localScale.y
                //         && collision.gameObject.transform.localScale.z <= checkPoint.transform.localScale.z) {
                //         WinScreen.SetActive(true);
                //         collision.gameObject.SetActive(false);
                //     }
                // }
                WinScreen.SetActive(true);
                collision.gameObject.SetActive(false);
                
                //post data if win
                stats.GetComponent<StatisticManager>().OnGameFinish();

            }
            else
            {
                LoseScreen.SetActive(true);
                collision.gameObject.SetActive(false);
                
                //post data if lose
                stats.GetComponent<StatisticManager>().OnGameFinish();
            }
        }
    }
}
