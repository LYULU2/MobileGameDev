using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointBehaviour : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerBehaviour>().Blue >= 4 &&
                collision.gameObject.GetComponent<PlayerBehaviour>().Yellow >= 3 &&
                collision.gameObject.GetComponent<PlayerBehaviour>().Red >= 6)
            {
                WinScreen.SetActive(true);
                collision.gameObject.SetActive(false);
            }
            else
            {
                LoseScreen.SetActive(true);
                collision.gameObject.SetActive(false);
            }
        }
    }
}
