using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/**
 * color chart:
 * yellow = (250, 240, 0, 255)
 * blue = (0, 136, 255, 255)
 * green = (0, 255, 0, 255)
 * red = (255, 0, 0, 255)
 * brown = ()
 * orange = ()
 * purple = (
 */

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject CheckPoint;
    public GameObject BlueBall;
    public GameObject YellowBall;
    public GameObject RedBall;
    public int Blue = 0;
    public int Yellow = 0;
    public int Red = 0;
    public float totalDistance = 0;
    public int SceneIndex;
    //private double barLength = 12.88075*2;
    private Vector3 StartPosition;
    private Vector3 oldPosition;
    private Vector3 colorBar1Position;
    private Vector3 colorBar2Position;
    private Color playerColor;
    public int packageCapacity;
    private int current_package_capacity;
    private GameObject[] colorBar;
    private  List<int> colorIndex = new List<int>();
    //0 = blue, 1 = yellow, 2 = red
    private Queue<int> colorQueue = new Queue<int>();
    //float[] cx = new float[2];
    //float cy, cz;
    //Reload same level

    public float distanceLastTP = 1;
    
    private Color colorBlue;
    private Color colorYellow;
    private Color colorRed;
    private Color colorGreen;
    private Color colorPurple;
    private Color colorBrown;
    private Color colorOrange;


    private void Start()
    {
        current_package_capacity = packageCapacity;
        StartPosition = gameObject.transform.position;
        oldPosition = gameObject.transform.position;
        colorBar = new GameObject[packageCapacity];
        colorQueue.Clear();
        colorIndex.Clear();
        for (int i = 0; i < packageCapacity; i++) {
            colorBar[i] = this.gameObject.transform.GetChild(i+1).gameObject;
            colorIndex.Add(i);
            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<SpriteRenderer>();

            // Call SetColor using the shader property name "_Color" and setting the color to white
            cubeRenderer.material.SetColor("_Color", Color.white);
        }
        
        //initialize colors
        colorBlue = new Color32(0, 136, 255, 255);
        colorYellow = new Color32(255, 240, 0, 255);
        colorGreen = new Color32(0, 255, 34, 255);
        colorPurple = new Color32(147,112,219, 255);
        colorBrown = new Color32(139,69,19, 255);
        colorOrange = new Color32(255,165,0,255);
        colorRed = new Color32(255, 0, 0, 255);
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
            for (int i = 0; i < 2 && colorQueue.Count > 0; i++)
            {
                int c = colorQueue.Dequeue();
                if (c == 0) Blue--;
                else if (c == 1)Yellow--;
                else if (c == 2) Red--;
            }
            updateColorBar();
            updatePlayerColor();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blue")
        {
            Blue = Blue + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(0);
        } 
        else if (collision.tag == "Yellow") 
        {
            Yellow = Yellow + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(1);
        }
        else if (collision.tag == "Red") 
        {
            Red = Red + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(2);
        }
        else if (collision.tag == "reducePackage")
        {
            collision.gameObject.SetActive(false);
            if (current_package_capacity > 2) {
                current_package_capacity-=2;
                colorQueue.Clear();
                if (current_package_capacity == 4) {
                    this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    this.gameObject.transform.GetChild(6).gameObject.SetActive(false);
                    colorIndex.RemoveAt(0);
                    colorIndex.RemoveAt(colorIndex.Count-1);

                } else {
                    this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    this.gameObject.transform.GetChild(5).gameObject.SetActive(false);
                    colorIndex.RemoveAt(0);
                    colorIndex.RemoveAt(colorIndex.Count-1);
                }
                Blue = Yellow = Red = 0;
            }
        } 
        if (colorQueue.Count > current_package_capacity) {
            int front = colorQueue.Dequeue();
            if (front == 0) Blue--;
            else if (front == 1) Yellow--;
            else Red--;
        }
        updatePlayerColor();
    }    
    private void updatePlayerColor()
    {
        if (Blue > Yellow && Blue > Red)
        {
            playerColor = colorBlue;
        }
        else if (Yellow > Blue && Yellow > Red)
        {
            playerColor = colorYellow;
        }
        else if (Blue == 0 && Yellow == 0 && Red == 0)
        {
            playerColor = Color.white;
        }
        else if (Red > Blue && Red > Yellow)
        {
            playerColor = colorRed;
        }
        else if (Blue == Yellow && Blue > Red)
        {
            playerColor = colorGreen;
        }
        else if (Red == Blue && Red > Yellow)
        {
            playerColor = colorPurple;
        }
        else if (Red == Yellow && Red > Blue)
        {
            playerColor = colorOrange;
        }
        else if (Red == Blue && Red == Yellow)
        {
            playerColor = colorBrown;
        }

        gameObject.GetComponent<SpriteRenderer>().color = playerColor;
        updateColorBar();
    }

    private void updateColorBar() 
    {
        int B = Blue, Y = Yellow, R = Red, total = B+Y+R, i = 0;
        // Debug.Log("Yellow: " + Yellow + " Blue: " + Blue+ " B: " + B);
        foreach (int c in colorQueue) {
            var cubeRenderer = colorBar[colorIndex[i]].GetComponent<SpriteRenderer>();
            if (c==0) cubeRenderer.color = colorBlue;
            else if (c==1) cubeRenderer.color = colorYellow;
            else if (c==2) cubeRenderer.color = colorRed;
            i++;
        }
        int left = current_package_capacity-colorQueue.Count;
        if (left > 0) {
            for (int k = colorIndex.Count-1, j = 0; k > -1 && j < left; k--, j++) {
                var cubeRenderer = colorBar[colorIndex[k]].GetComponent<SpriteRenderer>();
                cubeRenderer.color = Color.white;
            }
        }
        if (colorQueue.Count == 0) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void Reset()
    {
        transform.position = StartPosition;
        current_package_capacity = packageCapacity;
        oldPosition = gameObject.transform.position;
        colorBar = new GameObject[packageCapacity];
        colorIndex.Clear();
        for (int i = 0; i < packageCapacity; i++)
        {
            colorBar[i] = this.gameObject.transform.GetChild(i + 1).gameObject;
            this.gameObject.transform.GetChild(i+1).gameObject.SetActive(true);
            colorIndex.Add(i);
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
        Red = 0;
        gameObject.SetActive(true);
    }
}
