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
            if (collision.GetComponent<SpriteRenderer>().color == GameObject.FindGameObjectsWithTag("Finish")[0].GetComponent<SpriteRenderer>().color)
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
