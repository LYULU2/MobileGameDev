using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    
    public Text BlueCount;
    public Text YellowCount;
    public Text RedCount;
    public int Blue = 0;
    public int Yellow = 0;
    public int Red = 0;
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
        }
        if (collision.tag == "Yellow")
        {
            Yellow++;
            YellowCount.text = Yellow.ToString();
            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "Red")
        {
            Red++;
            RedCount.text = Red.ToString();
            collision.gameObject.SetActive(false);
        }
    }
}
