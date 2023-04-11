using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject _Colorbar_UI;
    public GameObject _Colorbar_Body;
    public GameObject powerLight;
    public GameObject superPowerIndicator;
    public int Blue = 0;
    public int Yellow = 0;
    public int Red = 0;
    public int reducePackage = 0;
    public int numHitEnemy = 0;
    public int numBounceEnemy = 0;
    public float totalDistance = 0;
    public int portalLockHit = 0;

    public int SceneIndex;

    private Vector3 StartScale;
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
    public GameObject bulletPrefab; // The prefab of the object to create
    public float force = 500f; // The force to apply to the object

    public float distanceLastTP = 1;
    
    private Color colorBlue;
    private Color colorYellow;
    private Color colorRed;
    private Color colorGreen;
    private Color colorPurple;
    private Color colorBrown;
    private Color colorOrange;

    public bool protectedByShield = false;

    public Queue<int> getColorQueue() {
        return colorQueue;
    }

    private void Start()
    {
        StartScale = transform.localScale;
        current_package_capacity = packageCapacity;
        StartPosition = gameObject.transform.position;
        oldPosition = gameObject.transform.position;
        colorBar = new GameObject[packageCapacity];
        colorQueue.Clear();
        colorIndex.Clear();
        for (int i = 0; i < packageCapacity; i++) {
            colorBar[i] = _Colorbar_UI.transform.GetChild(i).gameObject;
            colorIndex.Add(i);
            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<Image>();

            // Call SetColor using the shader property name "_Color" and setting the color to white
            cubeRenderer.material.SetColor("_Color", Color.white);
        }
        
        //initialize colors
        colorBlue = new Color32(0, 136, 255, 255);
        colorYellow = new Color32(255, 240, 0, 255);
        colorGreen = new Color32(50, 220, 85, 255);
        colorPurple = new Color32(147,112,219, 255);
        colorBrown = new Color32(139,69,19, 255);
        colorOrange = new Color32(255,165,0,255);
        colorRed = new Color32(166, 15, 15, 255);

        updateSuperPower();
    }
    private void Update() 
    {
        UpdateTotalDistance();
        if (Input.GetKeyDown(KeyCode.Space)) {
            ejectColor();
        }
    }
    private void ejectColor() {
        if (colorQueue.Count == 0) return;
        int c = colorQueue.Dequeue();
        Vector3 newPosition = transform.position;
        // Check if the object is facing right or left
        if (transform.right.x > 0)
        {
            // Object is facing right
            newPosition.x += 1.5f;
        } else {
            // Object is facing left
            newPosition.x -= 1.5f;
        }
        
        GameObject nb= Instantiate(bulletPrefab, newPosition, Quaternion.identity);
        if (c == 0) {
            Blue--;
            // Change the color of the new game object
            nb.GetComponent<SpriteRenderer>().color = colorBlue;
            // Change the tag of the new game object
            nb.tag = "blueBullet";
        } else if (c == 1) {
            Yellow--;
            // Change the color of the new game object
            nb.GetComponent<SpriteRenderer>().color =  colorYellow;
            // Change the tag of the new game object
            nb.tag = "yellowBullet";
        } else if (c == 2) {
            Red--;
            // Change the color of the new game object
            nb.GetComponent<SpriteRenderer>().color =  colorRed;
            // Change the tag of the new game object
            nb.tag = "redBullet";
        }
        updateColorBar();
        updatePlayerColor();
        if (transform.right.x > 0) {
            nb.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
        } else {
            nb.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);
        }
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
        //Debug.Log("bump into enemy !!!");
        if (collision.gameObject.tag == "Enemy")
        {
            Color col = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Debug.Log(col);
            Debug.Log(gameObject.GetComponent<SpriteRenderer>().color);
            //white eats enemies
            /* 
            if (gameObject.GetComponent<SpriteRenderer>().color == col) {
                protectedByShield = true;
                collision.gameObject.SetActive(false);
                Debug.Log("destroy enemy");
            }*/
            if (!protectedByShield) {
                numHitEnemy++;
                for (int i = 0; i < 2 && colorQueue.Count > 0; i++)
                {
                    int c = colorQueue.Dequeue();
                    if (c == 0) Blue--;
                    else if (c == 1)Yellow--;
                    else if (c == 2) Red--;
                }
                updateColorBar();
                updatePlayerColor();
                StartCoroutine(Flasher());
            } else {
                numBounceEnemy++;
            }   
        }
        //we can use it if we want to add bullet color back to the player
        encounterBullet(collision);
    }
    private void updateSuperPower() // check if the super power of the player is activated by checking the colorQueue to be all the same or not
    {
        bool same = false;
        if (colorQueue.Count == current_package_capacity) {
            int first = colorQueue.Peek();
            same = true;
            foreach (int c in colorQueue) {
                if (c != first) {
                    same = false;
                    break;
                }
            }
        }
        if (same) { // increase the scale of the player to (1.4, 1.4, 1.4)
            powerLight.GetComponent<powerUpLight>().updateUXLight(true);
            superPowerIndicator.SetActive(true);
            //Debug.Log("scale up");
            gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            // activate the shield
            protectedByShield = true;
            // add the mass of the player to 2000
            gameObject.GetComponent<Rigidbody2D>().mass = 2000;
            // change the sprite of the player from Capsule to the character sprite
            // gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("character");
        } else { // reduce to unit size
            powerLight.GetComponent<powerUpLight>().updateUXLight(false);
            superPowerIndicator.SetActive(false);
            //Debug.Log("scale down");
            gameObject.transform.localScale = StartScale;
            // deactivate the shield
            protectedByShield = false;
            // reduce the mass of the player to 1
            gameObject.GetComponent<Rigidbody2D>().mass = 1;
            // change the sprite of the player from shield to the Capsule sprite
            // gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Capsule");
        }
    }
    private void encounterBullet(Collision2D collision)
    {

        GameObject otherGameObject = collision.gameObject;
        //Debug.Log("its tag name is " + collision.gameObject.tag);
        if (collision.gameObject.tag == "ablueBullet")
        {
            Blue = Blue + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(0);
        } 
        else if (collision.gameObject.tag == "ayellowBullet") 
        {
            Yellow = Yellow + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(1);
        }
        else if (collision.gameObject.tag == "aredBullet") 
        {
            Red = Red + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(2);
        }
        
        while (colorQueue.Count > current_package_capacity) {
            int front = colorQueue.Dequeue();
            if (front == 0) Blue--;
            else if (front == 1) Yellow--;
            else Red--;
        }
        updatePlayerColor();
        updateSuperPower();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.tag == "Blue" || collision.tag == "blueBullet")
        {
            Blue = Blue + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(0);
        } 
        else if (collision.tag == "Yellow" || collision.tag == "yellowBullet") 
        {
            Yellow = Yellow + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(1);
        }
        else if (collision.tag == "Red" || collision.tag == "redBullet") 
        {
            Red = Red + 1;
            collision.gameObject.SetActive(false);
            colorQueue.Enqueue(2);
        }
        else if (collision.tag == "reducePackage")
        {
            reducePackage++;
            collision.gameObject.SetActive(false);
            if (current_package_capacity > 2) {
                current_package_capacity-=2;
                //colorQueue.Clear();
                if (current_package_capacity == 4) {
                    _Colorbar_UI.transform.GetChild(0).gameObject.SetActive(false);
                    _Colorbar_UI.transform.GetChild(5).gameObject.SetActive(false);
                    colorIndex.RemoveAt(0);
                    colorIndex.RemoveAt(colorIndex.Count-1);

                } else {
                    _Colorbar_UI.transform.transform.GetChild(1).gameObject.SetActive(false);
                    _Colorbar_UI.transform.transform.GetChild(4).gameObject.SetActive(false);
                    colorIndex.RemoveAt(0);
                    colorIndex.RemoveAt(colorIndex.Count-1);
                }
            }
        } else if (collision.tag == "resetColor") {
            Blue = Yellow = Red = 0;
            collision.gameObject.SetActive(false);
            colorQueue.Clear();
            updateColorBar();
            updatePlayerColor();
        }
        while (colorQueue.Count > current_package_capacity) {
            int front = colorQueue.Dequeue();
            if (front == 0) Blue--;
            else if (front == 1) Yellow--;
            else Red--;
        }
        updatePlayerColor();
        updateSuperPower();
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
            var cubeRenderer = colorBar[colorIndex[i]].GetComponent<Image>();
            if (c==0) cubeRenderer.color = colorBlue;
            else if (c==1) cubeRenderer.color = colorYellow;
            else if (c==2) cubeRenderer.color = colorRed;
            i++;
        }
        int left = current_package_capacity-colorQueue.Count;
        if (left > 0) {
            for (int k = colorIndex.Count-1, j = 0; k > -1 && j < left; k--, j++) {
                var cubeRenderer = colorBar[colorIndex[k]].GetComponent<Image>();
                cubeRenderer.color = Color.white;
            }
        }
        if (colorQueue.Count == 0) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void Reset()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.localScale = StartScale;
        transform.position = StartPosition;
        current_package_capacity = packageCapacity;
        oldPosition = gameObject.transform.position;
        colorBar = new GameObject[packageCapacity];
        colorIndex.Clear();
        for (int i = 0; i < packageCapacity; i++)
        {
            colorBar[i] = _Colorbar_UI.transform.transform.GetChild(i).gameObject;
            _Colorbar_UI.transform.transform.GetChild(i).gameObject.SetActive(true);
            colorIndex.Add(i);
            // Get the Renderer component from the new cube
            var cubeRenderer = colorBar[i].GetComponent<Image>();
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
        updateSuperPower();
    }
    IEnumerator Flasher() 
    {
        Color oldColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        Color flashColor = new Color(1, 1, 1, 0.7f);
        for (int i = 0; i < 2; i++)
        {
            GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(.3f);
            GetComponent<SpriteRenderer>().color = oldColor; 
            yield return new WaitForSeconds(.3f);
        }
    }
}

