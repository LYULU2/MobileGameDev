using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    private void Start()
    {
        BlueCount.text = Blue.ToString();
        YellowCount.text = Yellow.ToString();
        RedCount.text = Red.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blue")
        {
            Blue++;
            BlueCount.text = Blue.ToString();
            collision.gameObject.SetActive(false);
            // increase the size
            transform.localScale *= 1.1f;
        }
        if (collision.tag == "Yellow")
        {
            Yellow++;
            YellowCount.text = Yellow.ToString();
            collision.gameObject.SetActive(false);
            // increase the size
            transform.localScale *= 1.1f;
            // increase the speed
            // GetComponent<moveForward>().speed += 1.0f;
        }
        if (collision.tag == "Red")
        {
            Red++;
            RedCount.text = Red.ToString();
            collision.gameObject.SetActive(false);
            // increase the size
            transform.localScale *= 1.1f;
            // increase the speed
            // GetComponent<moveForward>().speed += 1.0f;
        }
        // update color based on the number of each color
        color = new Color((float)Red / 10, (float)Yellow / 10, (float)Blue / 10);
        GetComponent<SpriteRenderer>().color = color;
        // player die if the ratio of red to blue is greater than 2
        if (Red > Blue * 2)
        {
            Debug.Log("Red > Blue * 2");
            // output the red and blue value for debugging
            Debug.Log("Red: " + Red);
            Debug.Log("Blue: " + Blue);
            Destroy(gameObject);
        }
        // player die if the ratio of blue to yellow is greater than 2
        if (Blue > Yellow * 2)
        {
            Debug.Log("Blue > Yellow * 2");
            // output the blue and yellow value for debugging
            Debug.Log("Blue: " + Blue);
            Debug.Log("Yellow: " + Yellow);
            Destroy(gameObject);
        }
        // player die if the ratio of yellow to red is greater than 2
        if (Yellow > Red * 2)
        {
            Debug.Log("Yellow > Red * 2");
            // output the yellow and red value for debugging
            Debug.Log("Yellow: " + Yellow);
            Debug.Log("Red: " + Red);
            Destroy(gameObject);
        }
    }
}
