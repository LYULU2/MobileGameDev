using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ColorUtility;

public class teleport : MonoBehaviour
{
    private BoxCollider2D teleportPortal;
    public GameObject Stats;
	private bool sentData;

    // Input the coordinate to teleport to
    public float x;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        Stats = GameObject.FindGameObjectsWithTag("Stat")[0];
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        teleportPortal = gameObject.GetComponent<BoxCollider2D>();
        //Here the GameObject's Collider is not a trigger
        teleportPortal.isTrigger = false;
        //Output whether the Collider is a trigger type Collider or not
        //Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            // tepleport player to x,y
            collision.transform.position = new Vector2(x, y);
            //Debug.Log("Teleporting");
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        teleportPortal.isTrigger = false;
    }
}
