using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public float decreaseDistance = 1.0f;
    public float decreaseScale_Shit = 0.05f;
    public float decreaseScale = 0.05f;
    public float increaseScale = 0.05f;
    public float loseScale_Small = 0.01f;
    public float loseScale_Big = 3.0f;

    public GameObject BlueBall;
    public GameObject YellowBall;
    public GameObject CheckPoint;
    private GameObject[] colorBar = new GameObject[4];
    public Text BlueCount;
    public Text YellowCount;

    // public Text RedCount;
    private int Blue = 0;
    private int Yellow = 0;
    // public int Red = 1;
    private Color playerColor;
    Vector3 posStart;
    // public float shrinkDistance; // distance that haven't eat
    public Vector3 oldPosition;
    public Vector3 lastEatPosition;
    public float totalDistance = 0;
    public float lastEatDistance = 0;
    // public GameObject LoseScreen_Big;
    // public GameObject LoseScreen_Small;
    // public GameObject player;
    
    private void Start()
    {
        BlueCount.text = Blue.ToString();
        YellowCount.text = Yellow.ToString();
        // RedCount.text = Red.ToString();
        posStart = transform.position;
        oldPosition = transform.position;
        lastEatPosition = transform.position;
        Blue = Yellow = 0;
        for (int i = 0; i < 4; i++) {
            colorBar[i] = this.gameObject.transform.GetChild(i+1).gameObject;
            
            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<Renderer>();

            // Call SetColor using the shader property name "_Color" and setting the color to red
            cubeRenderer.material.SetColor("_Color", Color.white);
        }
        
    }
    private void Update() {
        // checkShit(decreaseScale_Shit);

        //calculate player's walking distance
        Vector3 distanceVector = transform.position - oldPosition;
        float distance = distanceVector.magnitude;
        totalDistance += distance;
        
        //calculate gameObject's hungry distance
        Vector3 distanceVector2 = transform.position - lastEatPosition;
        float distance2 = distanceVector2.magnitude;
        lastEatDistance = distance2;
        oldPosition = transform.position;
        //need to decrease player's size when it move xx grids
        // shrinkDistance = Vector2.Distance(posStart, transform.position);
        // if (lastEatDistance >= decreaseDistance)
        // {
        //     DecreaseScale(decreaseScale);
        //     // transform.localScale -= new Vector3(decreaseScale, decreaseScale, decreaseScale);
        //     // // transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        //     // posStart = transform.position;
        //     // lastEatDistance = 0;
        //     // lastEatPosition = transform.position;
        // }
        // if (transform.localScale.x < loseScale_Small || transform.localScale.y < loseScale_Small || transform.localScale.z < loseScale_Small) {
        //     LoseScreen_Small.SetActive(true);
        //     Destroy(gameObject);
        // }
        // if (transform.localScale.x > loseScale_Big || transform.localScale.y > loseScale_Big || transform.localScale.z > loseScale_Big) {
        //     Debug.Log("Eat Too Much!!!!!!");
        //     LoseScreen_Big.SetActive(true);
        //     Destroy(gameObject);
        // }
    }
    private void checkShit(float decreaseScale_Shit)
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");
            DecreaseScale(decreaseScale_Shit);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            // color = Color.white;
        }
    }
    public void DecreaseScale(float decreaseScale)
    {
        transform.localScale -= new Vector3(decreaseScale, decreaseScale, decreaseScale);
        // transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        posStart = transform.position;
        lastEatDistance = 0;
        lastEatPosition = transform.position;

    }
    private void updateColorBar() {
        int B = 2, Y = 2, i = 0;
        if (Blue + Yellow < 4) {
            B = Blue; Y = Yellow;
        } else {
            if (Blue != Yellow) {
                if (Yellow > Blue) {
                    if (Yellow - Blue == 1) {
                        Y = 3; B = 1;
                    } else Y = 4;
                } else {
                    if (Blue - Yellow == 1) {
                        B = 3; Y = 1;
                    } else B = 4;
                }
            } 
        }
        for (int j = 0; j < B; j++) {
                var cubeRenderer = colorBar[i].GetComponent<Renderer>();
                cubeRenderer.material.SetColor("_Color", Color.blue);
                i++;
            }
        for (int j = 0; j < Y; j++) {
            var cubeRenderer = colorBar[i].GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.yellow);
            i++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blue")
        {
            //color = collision.GetComponent<SpriteRenderer>().color;
            Blue = Blue + 1;
            BlueCount.text = Blue.ToString();
            collision.gameObject.SetActive(false);
            //player eat
            lastEatDistance = 0;
            lastEatPosition = transform.position;
            // increase the size
            // transform.localScale += new Vector3(increaseScale, increaseScale, increaseScale);
            //transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        } else if (collision.tag == "Yellow") {
            //color = collision.GetComponent<SpriteRenderer>().color;
            Yellow = Yellow + 1;
            YellowCount.text = Yellow.ToString();
            collision.gameObject.SetActive(false);
            //player eat
            lastEatDistance = 0;
            lastEatPosition = transform.position;
            // increase the size
            // transform.localScale += new Vector3(increaseScale, increaseScale, increaseScale);
            // transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            // increase the speed
            // GetComponent<moveForward>().speed += 1.0f;
        }
        // } else if (collision.tag == "Red") {
        //     color = collision.GetComponent<SpriteRenderer>().color;
        //     RedCount.text = Red.ToString();
        //     collision.gameObject.SetActive(false);
        //     //player eat
        //     lastEatDistance = 0;
        //     lastEatPosition = transform.position;
        //     // increase the size
        //     transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        //     // increase the speed
        //     // GetComponent<moveForward>().speed += 1.0f;
        // }
        // if (transform.localScale.x > loseScale_Big || transform.localScale.y > loseScale_Big || transform.localScale.z > loseScale_Big) {
        //     Debug.Log("Ofcourse it works!");
        //     transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        // }
        //if (transform.localScale > upLimit) transform.localScale = upLimit;
        //transform.localScale = Mathf.Max(transform.localScale, upLimit);

        // update color based on the number of each color
        if (Blue > Yellow)
        {
            playerColor = BlueBall.GetComponent<SpriteRenderer>().color;
        }
        else if (Yellow > Blue)
        {
            playerColor = YellowBall.GetComponent<SpriteRenderer>().color;
        }
        else if (Blue == Yellow)
        {
            playerColor = CheckPoint.GetComponent<SpriteRenderer>().color;
        }
        gameObject.GetComponent<SpriteRenderer>().color = playerColor;
        updateColorBar();
    }
    
}
