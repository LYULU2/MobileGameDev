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
            color = new Color(0, 0, 1.0f);
            BlueCount.text = Blue.ToString();
            collision.gameObject.SetActive(false);
            // increase the size
            transform.localScale *= 1.1f;
        }
        if (collision.tag == "Yellow")
        {
            color = new Color(1.0f, 1.0f, 0);
            YellowCount.text = Yellow.ToString();
            collision.gameObject.SetActive(false);
            // increase the size
            transform.localScale *= 1.1f;
            // increase the speed
            // GetComponent<moveForward>().speed += 1.0f;
        }
        if (collision.tag == "Red")
        {
            color = new Color(1.0f, 0, 0);
            RedCount.text = Red.ToString();
            collision.gameObject.SetActive(false);
            // increase the size
            transform.localScale *= 1.1f;
            // increase the speed
            // GetComponent<moveForward>().speed += 1.0f;
        }
        // update color based on the number of each color
        
        GetComponent<SpriteRenderer>().color = color;
    }
}
