using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passThrough_or_Block : MonoBehaviour
{
    private new BoxCollider2D wall;
    private GameObject player;
    Color red = new Color(1.0f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        wall = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        //Here the GameObject's Collider is not a trigger
        wall.isTrigger = false;
        //Output whether the Collider is a trigger type Collider or not
        //Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(" OnCollisionEnter2D ");
        Color palyerColor = player.GetComponent<SpriteRenderer>().color;
        Debug.Log(palyerColor.r);
        Debug.Log(palyerColor.g);
        Debug.Log(palyerColor.b);
        if (palyerColor.r == 1.0 && palyerColor.g == 0.0 && palyerColor.b == 0)
        {
            wall.isTrigger = true;
        } else wall.isTrigger = false;
    }
}
