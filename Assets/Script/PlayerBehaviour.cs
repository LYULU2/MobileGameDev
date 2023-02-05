using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    
    public Text BlueCount;
    public Text YellowCount;
    public Text RedCount;
    public int Blue = 1;
    public int Yellow = 1;
    public int Red = 1;
    public Color color;
    Vector3 posStart;
    public float shrinkDistance; // distance that haven't eat
    public Vector3 oldPosition;
    public Vector3 lastEatPosition;
    public float totalDistance = 0;
    public float lastEatDistance = 0;
    public GameObject LoseScreen;
    public GameObject player;
    
    private void Start()
    {
        BlueCount.text = Blue.ToString();
        YellowCount.text = Yellow.ToString();
        RedCount.text = Red.ToString();
        posStart = transform.position;
        oldPosition = transform.position;
        lastEatPosition = transform.position;
    }
    private void Update() {
        //calculate player's walking distance
        Vector3 distanceVector = transform.position - oldPosition;
        float distance = distanceVector.magnitude;
        totalDistance += distance;
        
        //calculate player's hungry distance
        Vector3 distanceVector2 = transform.position - lastEatPosition;
        float distance2 = distanceVector2.magnitude;
        lastEatDistance = distance2;
        oldPosition = transform.position;
        //need to decrease player's size when it move xx grids
        shrinkDistance = Vector2.Distance(posStart, transform.position);
        if (lastEatDistance >= 1)
        {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            posStart = transform.position;
            lastEatDistance = 0;
            lastEatPosition = transform.position;
        }
            if (transform.localScale.x < 0.01f || transform.localScale.y < 0.01f || transform.localScale.z < 0.01f) {
            LoseScreen.SetActive(true);
            Destroy(gameObject);
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blue")
        {
            color = collision.GetComponent<SpriteRenderer>().color;
            BlueCount.text = Blue.ToString();
            collision.gameObject.SetActive(false);
            //player eat
            lastEatDistance = 0;
            lastEatPosition = transform.position;
            // increase the size
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        } else if (collision.tag == "Yellow") {
            color = collision.GetComponent<SpriteRenderer>().color;
            YellowCount.text = Yellow.ToString();
            collision.gameObject.SetActive(false);
            //player eat
            lastEatDistance = 0;
            lastEatPosition = transform.position;
            // increase the size
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            // increase the speed
            // GetComponent<moveForward>().speed += 1.0f;
        } else if (collision.tag == "Red") {
            color = collision.GetComponent<SpriteRenderer>().color;
            RedCount.text = Red.ToString();
            collision.gameObject.SetActive(false);
            //player eat
            lastEatDistance = 0;
            lastEatPosition = transform.position;
            // increase the size
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            // increase the speed
            // GetComponent<moveForward>().speed += 1.0f;
        }
        if (transform.localScale.x > 3.0f || transform.localScale.y > 3.0f || transform.localScale.z > 3.0f) {
            transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
        //if (transform.localScale > upLimit) transform.localScale = upLimit;
        //transform.localScale = Mathf.Max(transform.localScale, upLimit);
        // update color based on the number of each color
        
        GetComponent<SpriteRenderer>().color = color;
    }
    
}
