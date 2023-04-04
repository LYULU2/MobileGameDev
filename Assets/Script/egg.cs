using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg : MonoBehaviour
{
    public bool hidden = false;
    // Start is called before the first frame update
    void Start()
    {
        hidden = true;
    }

    void Update()
    {
        if (hidden == true) // hide the object if the hidden is true
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // output the collision tag
        Debug.Log(collision.gameObject.tag);
        // if collide with an enemy, set the hidden to false and destory the enemy
        if (collision.gameObject.tag == "Enemy")
        {
            hidden = false;
            Destroy(collision.gameObject);
        } else if (!hidden && collision.gameObject.tag == "Player") // finish the game if the egg is not hidden and collide with the player
        {
            GameObject.Find("UI_Manager").GetComponent<UIManager>().ShowWinMenu();
            collision.gameObject.SetActive(false);
        }
    }

    public void Reset() {
        hidden = true;
    }
}
