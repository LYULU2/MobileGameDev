using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject[] Collectables;
    public int SceneIndex;
    public GameObject CheckPoint;
    RESET RR;
    // public GameObject WinScreen;
    public GameObject ResetButton;

    private GameObject player;
    public void Reset()
    {
        foreach(GameObject collectable in Collectables)
        {
            collectable.gameObject.SetActive(true);
        }
    }
    GameObject[] FindGameObjectsWithTags(params string[] tags)
    {
        var all = new List<GameObject>();

        foreach (string tag in tags)
        {
            all.AddRange(GameObject.FindGameObjectsWithTag(tag).ToList());
        }

        return all.ToArray();
    }
    // Start is called before the first frame update
    void Start()
    {
        RR = ResetButton.GetComponent<RESET>();
        string[] para = new string[5] {"Blue","Yellow","Red","reducePackage", "resetColor"};
        Collectables = FindGameObjectsWithTags(para);
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    /**
     * This part is confusing, player won't even realize what happened
     */
    private void finishCollect() {
        //deal with the situation when all collectables' active is false and player's color is not match to the end
        bool valid = true;
        for (int i = 0; i < Collectables.Length; i++) {
            if (Collectables[i].activeSelf) {
                valid = false;
                break;
            }
        }
        if (valid) {
            //Debug.Log("finish collect!");

			//TODO
			/**
			* Might not just be one end
			*/
            
            Color playerColor = player.GetComponent<SpriteRenderer>().color;
            Color finisher = GameObject.FindGameObjectsWithTag("Finish")[0].GetComponent<SpriteRenderer>().color;
            string playerColorHex = ColorUtility.ToHtmlStringRGBA(playerColor);
            string wallColorHex = ColorUtility.ToHtmlStringRGBA(finisher);
            if (playerColorHex != wallColorHex) ResetButton.GetComponent<RESET>().Reset();
        }
    }
    void resetBullet() {
        // Find all GameObjects with the "blueBullet" tag
        GameObject[] blueBullets = GameObject.FindGameObjectsWithTag("ablueBullet");

        // Loop through all blueBullets and destroy them
        foreach (GameObject bullet in blueBullets) {
            Destroy(bullet);
        }
        
        GameObject[] yellowBullets = GameObject.FindGameObjectsWithTag("ayellowBullet");

        // Loop through all blueBullets and destroy them
        foreach (GameObject bullet in yellowBullets) {
            Destroy(bullet);
        }
        GameObject[] redBullets = GameObject.FindGameObjectsWithTag("aredBullet");

        // Loop through all blueBullets and destroy them
        foreach (GameObject bullet in redBullets) {
            Destroy(bullet);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //finishCollect();
        if (Input.GetKeyDown(KeyCode.R)) {
            // Do something when the 'R' key is pressed
            RR.resetBullet();
            //resetBullet();
        }
        // if (WinScreen.activeSelf == true)
        // {
        //     int currentIndex = SceneManager.GetActiveScene().buildIndex;
        //     if (currentIndex +1 < SceneManager.sceneCountInBuildSettings && Input.GetKeyDown(KeyCode.N))
        //     {
        //         SceneManager.LoadScene(currentIndex + 1);
        //     }
        // }
    }
}
