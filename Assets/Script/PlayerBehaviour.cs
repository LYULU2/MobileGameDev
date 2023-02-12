using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject CheckPoint;
    public GameObject BlueBall;
    public GameObject YellowBall;
    public int Blue = 0;
    public int Yellow = 0;
    public float totalDistance = 0;
    public Color color1;
    public Color color2;
    public int SceneIndex;

    private Vector3 StartPosition;
    private Vector3 oldPosition;
    private Color playerColor;
    private GameObject[] colorBar = new GameObject[10];
    

    //Reload same level


    private void Start()
    {
        StartPosition = gameObject.transform.position;
        oldPosition = gameObject.transform.position;
 
        for (int i = 0; i < 10; i++) {
            colorBar[i] = this.gameObject.transform.GetChild(i+1).gameObject;
            
            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<SpriteRenderer>();

            // Call SetColor using the shader property name "_Color" and setting the color to red
            //cubeRenderer.material.SetColor("_Color", Color.white);
        }     
    }
    private void Update() 
    {
        UpdateTotalDistance();
    }

    private void UpdateTotalDistance()
    {
        //calculate player's total walking distance
        Vector3 distanceVector = gameObject.transform.position - oldPosition;
        float distance = distanceVector.magnitude;
        totalDistance += distance;
        oldPosition = gameObject.transform.position;
        //Debug.Log("we walk" + totalDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blue")
        {
            Blue = Blue + 1;
            collision.gameObject.SetActive(false);
        } 
        else if (collision.tag == "Yellow") 
        {
            Yellow = Yellow + 1;
            collision.gameObject.SetActive(false);
        }

        // update player's color based on the number of collected balls
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

    private void updateColorBar() 
    {
        int B = Blue, Y = Yellow;
        if (Yellow == 0) B = 10;
        else if (Blue == 0) Y = 10;
        else if (Blue == Yellow) {
            B = Y = 5;
        } else {
            //Y = 10*Yellow/total;
            B = 10*Blue/(Yellow + Blue);
        }
        //Debug.Log("Yellow: " + Yellow + " Blue: " + Blue+ " B: " + B);
        for (int j = 0; j < B; j++) {
            var cubeRenderer = colorBar[j].GetComponent<SpriteRenderer>();
            cubeRenderer.color = color1;
        }
        for (int j = B; j < 10; j++) {
            var cubeRenderer = colorBar[j].GetComponent<SpriteRenderer>();
            cubeRenderer.color = color2;
        }
    }
    public void Reset()
    {
        transform.position = StartPosition;
        oldPosition = gameObject.transform.position;
        colorBar = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            colorBar[i] = this.gameObject.transform.GetChild(i + 1).gameObject;

            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<SpriteRenderer>();
            cubeRenderer.color = Color.white;

            // Call SetColor using the shader property name "_Color" and setting the color to red
            //cubeRenderer.material.SetColor("_Color", Color.white);
        }
        totalDistance = 0;
        playerColor = Color.white;
        gameObject.GetComponent<SpriteRenderer>().color = playerColor;
        Blue = 0;
        Yellow = 0;
        gameObject.SetActive(true);
    }
}
