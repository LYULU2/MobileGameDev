using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ColorUtility;

public class passThrough_or_Block : MonoBehaviour
{
    private BoxCollider2D wall;
    public GameObject Stats;
    // Start is called before the first frame update
    void Start()
    {
        Stats = GameObject.FindGameObjectsWithTag("Stat")[0];
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

         //Debug.Log(playerColorHex);
         //Debug.Log(wallColorHex);

        if (collision.transform.tag == "Player" && playerColorHex == wallColorHex)
        {
            wall.isTrigger = true;
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //wall.isTrigger = false;
    }
}
