using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ColorUtility;

public class passThrough_or_Block : MonoBehaviour
{
    private BoxCollider2D wall;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        wall = gameObject.GetComponent<BoxCollider2D>();
        //Here the GameObject's Collider is not a trigger
        wall.isTrigger = false;
        //Output whether the Collider is a trigger type Collider or not
        //Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Color playerColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
        Color wallColor = gameObject.GetComponent<SpriteRenderer>().color;

        string playerColorHex = ColorUtility.ToHtmlStringRGBA(playerColor);
        string wallColorHex = ColorUtility.ToHtmlStringRGBA(wallColor);

        // Debug.Log(playerColorHex);
        // Debug.Log(wallColorHex);

        if (collision.transform.tag == "Player" && playerColorHex == wallColorHex)
        {
            wall.isTrigger = true;
        } 
        // Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().color.linear);
        // Debug.Log(gameObject.GetComponent<SpriteRenderer>().color.linear);    
        // playerColor.r = Mathf.Round(playerColor.r * 10) / 10;
        // playerColor.b = Mathf.Round(playerColor.b * 10) / 10;
        // playerColor.g = Mathf.Round(playerColor.g * 10) / 10;
        // Color wallColor = gameObject.GetComponent<SpriteRenderer>().color;
        // wallColor.r = Mathf.Round(wallColor.r * 10) / 10;
        // wallColor.b = Mathf.Round(wallColor.b * 10) / 10;
        // wallColor.g = Mathf.Round(wallColor.g * 10) / 10;
        // if (collision.transform.tag == "Player" &&  playerColor == wallColor)
        // {
        //     wall.isTrigger = true;
        // } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        wall.isTrigger = false;
    }
}
