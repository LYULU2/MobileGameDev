using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color myColor;
	public GameObject blueBallPrefab;
	public GameObject redBallPrefab;
	public GameObject YellowBallPrefab;
	public Dictionary<string, GameObject> color2Prefab;
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
		color2Prefab = new Dictionary<string, GameObject>();
		color2Prefab.Add("0088FFFF", blueBallPrefab);
		color2Prefab.Add("FFF000FF", YellowBallPrefab);
		color2Prefab.Add("A60F0FFF", redBallPrefab);
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
            //change enemy's position
            // Get the current position of the object
            Vector3 currentPosition = collision.gameObject.transform.position;

            // Set the x position to a new value
            //float newXPosition = 5.0f;
            //currentPosition.x = currentPosition.x+newXPosition;

			collision.gameObject.SetActive(false);
			
			Instantiate(color2Prefab[ColorUtility.ToHtmlStringRGBA(myColor)], currentPosition, Quaternion.identity);
            // Update the position of the object
            collision.gameObject.transform.position = currentPosition;
        } else if (myTag!="" && myTag[0] != 'a' && collision.gameObject.tag == "Untagged") {
	        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.tag = "a"+myTag;
        }
        
        //gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
