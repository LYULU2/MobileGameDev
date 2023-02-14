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
    private GameObject[] colorBar = new GameObject[10];
    private GameObject[] tcb;
    float[] cx = new float[2];
    float cy, cz;
    //Reload same level


    private void Start()
    {
        StartPosition = gameObject.transform.position;
        oldPosition = gameObject.transform.position;
        tcb = GameObject.FindGameObjectsWithTag("tcb");
        for (int i = 0; i < 10; i++) {
            colorBar[i] = this.gameObject.transform.GetChild(i+1).gameObject;
            
            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<SpriteRenderer>();

            // Call SetColor using the shader property name "_Color" and setting the color to red
            //cubeRenderer.material.SetColor("_Color", Color.white);
        }
        colorBar2Position = tcb[0].transform.position;
        colorBar1Position = tcb[1].transform.position;
        cx[0] = colorBar1Position.x;
        cx[1] = colorBar2Position.x;
        cy = colorBar2Position.y;
        cz = colorBar2Position.z;
        //workaround ... not find root cause yet ...
        if (cx[0] > cx[1]) {
            float tmp = cx[1];
            cx[1] = cx[0];
            cx[0] = tmp;
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
        updateTopColorBar();
    }
    private void updateTopColorBar() {
        //tcb 0 = blue, 1 = yellow
        if (Yellow == 0) {
            var cubeRenderer = tcb[1].GetComponent<SpriteRenderer>();
            cubeRenderer.color = color1;
            cubeRenderer = tcb[0].GetComponent<SpriteRenderer>();
            cubeRenderer.color = color1;
            tcb[0].transform.position = new Vector3(cx[1], cy, cz);
            tcb[1].transform.position = new Vector3(cx[0], cy, cz);
        } else if (Blue == 0) {
            var cubeRenderer = tcb[0].GetComponent<SpriteRenderer>();
            cubeRenderer.color = color2;
            cubeRenderer = tcb[1].GetComponent<SpriteRenderer>();
            cubeRenderer.color = color2;
            tcb[0].transform.position = new Vector3(cx[1], cy, cz);
            tcb[1].transform.position = new Vector3(cx[0], cy, cz);
        }
        else if (Blue == Yellow) {
            tcb[0].transform.localScale = new Vector3(12.88075f, 1.880215f, 1f);
            tcb[1].transform.localScale = new Vector3(12.88075f, 1.880215f, 1f);
            var c2 = tcb[0].GetComponent<SpriteRenderer>();
            c2.color = color2;
            var c1 = tcb[1].GetComponent<SpriteRenderer>();
            c1.color = color1;
            tcb[0].transform.position = new Vector3(cx[1], cy, cz);
            tcb[1].transform.position = new Vector3(cx[0], cy, cz);
        } else {
            //Y = 10*Yellow/total;
            float B = 25.7615f*(float)Blue/(float)(Yellow + Blue);
            float Y = 25.7615f-B;
            float start = cx[0]-6.4404f;
            Debug.Log(cx[0]+ " : " + cx[1]);
            Debug.Log(B + ", " + Y + " : " + start);
            tcb[1].transform.localScale = new Vector3(B, 1.880215f, 1f);
            tcb[0].transform.localScale = new Vector3(Y, 1.880215f, 1f);
            tcb[1].transform.position = new Vector3(B/2+start, cy, cz);
            tcb[0].transform.position = new Vector3(start+B+Y/2, cy, cz);

            var c2 = tcb[0].GetComponent<SpriteRenderer>();
            c2.color = color2;
            var c1 = tcb[1].GetComponent<SpriteRenderer>();
            c1.color = color1;
        }
        // tcb[0].transform.position = colorBar2Position;
        // //tcb[0].transform.position.x = tcb[0].transform.localScale.x+cx[0];
        // tcb[1].transform.position = colorBar1Position;
        //tcb[1].transform.position.x = tcb[1].transform.localScale.x+cx[1];
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
