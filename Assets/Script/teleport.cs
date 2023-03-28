using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ColorUtility;

public class teleport : MonoBehaviour
{
    private BoxCollider2D teleportPortal;
    public GameObject Stats;
	private bool sentData;
    [SerializeField] AudioClip errorSFX;
    [SerializeField] private Sprite unlockSprite;
    [SerializeField] GameObject[] keys;
    [SerializeField] private Sprite lockSprite;

    // Input the coordinate to teleport to
    public float x;
    public float y;


    public bool init_unlocked = true; // whether this tp is unlocked at inital state or not
    private bool unlocked = false; // whether the teleport is already unlocked or not

    // the list of color blocks that is the key to unlock the 
    private Queue<int> colorQueue = new Queue<int>();
    // the string that specify the colorQueue
    public string colorQueueString = "B";
    

    private void parseColorQueue(string colorString) {
        // clear the colorQueue
        colorQueue.Clear();
        // enumerate the string and add the color to the colorQueue
        foreach (char c in colorString) {
            switch (c) {
                case 'B':
                    colorQueue.Enqueue(0);
                    break;
                case 'Y':
                    colorQueue.Enqueue(1);
                    break;
                case 'R':
                    colorQueue.Enqueue(2);
                    break;
                default:
                    Debug.Log("Invalid color in colorQueueString");
                    break;
            }
        }
    }

    private string getColorQueueString(Queue<int> s) {
        // transform the colorQueue to string
        string colorString = "";
        foreach (int i in s) {
            switch (i) {
                case 0:
                    colorString += "B";
                    break;
                case 1:
                    colorString += "Y";
                    break;
                case 2:
                    colorString += "R";
                    break;
                default:
                    Debug.Log("Invalid color in colorQueue");
                    break;
            }
        }
        // print colorString
        Debug.Log(colorString);
        return colorString;
    }

    // Start is called before the first frame update
    void Start()
    {
        lockSprite = GetComponent<SpriteRenderer>().sprite;
        keys = new GameObject[6];
        Stats = GameObject.FindGameObjectsWithTag("Stat")[0];
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        teleportPortal = gameObject.GetComponent<BoxCollider2D>();
        //Here the GameObject's Collider is not a trigger
        teleportPortal.isTrigger = true;
        unlocked = init_unlocked;
        parseColorQueue(colorQueueString);
        for (int i = 0; i < 6; i++)
        {

            keys[i] = transform.GetChild(i).gameObject;
        }
        //Output whether the Collider is a trigger type Collider or not
        //Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);
    }

    // the function used to call the update on the UX
    void updateUX()
    {
        GetComponent<SpriteRenderer>().sprite = unlockSprite;
        foreach(GameObject key in keys)
        {
            key.SetActive(false);
        }
        return ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // deactive the current object if collide with an enemy
        if (collision.transform.tag == "Enemy")
        {
            gameObject.SetActive(false);
            return ;
        }
        Debug.Log(unlocked);

        // unlock the teleport if collide with player, and the colorQueue of the player is the same as the colorQueue of the teleport
        if (collision.transform.tag == "Player" && !unlocked )
        {
            if (getColorQueueString(collision.transform.GetComponent<PlayerBehaviour>().getColorQueue()) == getColorQueueString(colorQueue)) {
                unlocked = true;
                updateUX();
            }
            else
            {
                AudioSource.PlayClipAtPoint(errorSFX, gameObject.transform.position, 100f);
            }
        }

        // teleport the player to the destination if collide with the player
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<PlayerBehaviour>().distanceLastTP > 1.0f)
        {
            if (!unlocked)
            {
                Debug.Log("Teleport is locked");
                collision.transform.GetComponent<PlayerBehaviour>().portalLockHit += 1;
                return ;
            }
            // tepleport player to x,y
            collision.transform.position = new Vector2(x, y);
            collision.transform.GetComponent<PlayerBehaviour>().distanceLastTP = 0;
            //Debug.Log("Teleporting");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        teleportPortal.isTrigger = false;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        unlocked = init_unlocked;
        parseColorQueue(colorQueueString);
        foreach(GameObject key in keys)
        {
            key.SetActive(true);
        }
        GetComponent<SpriteRenderer>().sprite = lockSprite;
    }
}
