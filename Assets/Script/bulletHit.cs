using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color myColor;
    string myTag ="";
    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Get the prefab's own color
        myColor = spriteRenderer.color;
        myTag = gameObject.tag;
        // Do something with the color
        Debug.Log("myTag is " + myTag);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bullet hit " + collision.gameObject.tag + " its own tag name is " + myTag);
        // Check if the collision object has a tag called "Obstacle"
        if (collision.gameObject.tag == "Enemy")
        {
            // Get the SpriteRenderer component attached to the collided object
            SpriteRenderer enemyRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

            // Change the color of the collided object to red
            enemyRenderer.color = myColor;
            // Destroy the object that this script is attached to
            gameObject.SetActive(false);
        } else if (myTag!="" && myTag[0] != 'a' && collision.gameObject.tag == "Untagged") {
            gameObject.tag = "a"+myTag;
        }
        
        //gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
