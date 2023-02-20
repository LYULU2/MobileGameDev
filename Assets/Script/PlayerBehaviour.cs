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
    private double barLength = 12.88075*2;
    private Vector3 StartPosition;
    private Vector3 oldPosition;
    private Vector3 colorBar1Position;
    private Vector3 colorBar2Position;
    private Color playerColor;
    public int packageCapacity;
    private GameObject[] colorBar;
    //true = blue, false = yellow
    private Queue<bool> colorQueue = new Queue<bool>();
    float[] cx = new float[2];
    float cy, cz;
    //Reload same level

    public float distanceLastTP = 1;


    private void Start()
    {
        StartPosition = gameObject.transform.position;
        oldPosition = gameObject.transform.position;
        colorBar = new GameObject[packageCapacity];
        colorQueue.Clear();
        for (int i = 0; i < packageCapacity; i++) {
            colorBar[i] = this.gameObject.transform.GetChild(i+1).gameObject;
            
            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<SpriteRenderer>();

            // Call SetColor using the shader property name "_Color" and setting the color to red
            cubeRenderer.material.SetColor("_Color", Color.white);
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
        if (distance < 1.0f) {
            distanceLastTP += distance;
        }
        oldPosition = gameObject.transform.position;
        //Debug.Log("we walk" + totalDistance);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy") {
            for (int i = 0; i < 2 && colorQueue.Count > 0; i++) {
                if (colorQueue.Dequeue()) Blue--;
                else Yellow--;
            }
            updateColorBar();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blue")
        {
            Blue = Blue + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(true);
        } 
        else if (collision.tag == "Yellow") 
        {
            Yellow = Yellow + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(false);
        }
        if (colorQueue.Count > 6) {
            bool front = colorQueue.Dequeue();
            if (front) Blue--;
            else Yellow--;
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
        else if (Blue == 0 && Yellow == 0)
        {
            playerColor = Color.white;
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
        int B = Blue, Y = Yellow, total = B+Y, i = 0;
        // Debug.Log("Yellow: " + Yellow + " Blue: " + Blue+ " B: " + B);
        foreach (bool blue in colorQueue) {
            var cubeRenderer = colorBar[i].GetComponent<SpriteRenderer>();
            if (blue) cubeRenderer.color = color1;
            else cubeRenderer.color = color2;
            i++;
        }
        int left = packageCapacity-colorQueue.Count;
        if (left > 0) {
            for (int k = packageCapacity-1, j = 0; k > -1 && j < left; k--, j++) {
                var cubeRenderer = colorBar[k].GetComponent<SpriteRenderer>();
                cubeRenderer.color = Color.white;
            }
        }
    }
    public void Reset()
    {
        transform.position = StartPosition;
        oldPosition = gameObject.transform.position;
        colorBar = new GameObject[packageCapacity];
        for (int i = 0; i < packageCapacity; i++)
        {
            colorBar[i] = this.gameObject.transform.GetChild(i + 1).gameObject;

            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<SpriteRenderer>();
            cubeRenderer.color = Color.white;

            // Call SetColor using the shader property name "_Color" and setting the color to red
            //cubeRenderer.material.SetColor("_Color", Color.white);
        }
        colorQueue.Clear();
        totalDistance = 0;
        playerColor = Color.white;
        gameObject.GetComponent<SpriteRenderer>().color = playerColor;
        Blue = 0;
        Yellow = 0;
        gameObject.SetActive(true);
    }
}
