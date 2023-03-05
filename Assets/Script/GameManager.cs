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
        string[] para = new string[4] {"Blue","Yellow","Red","reducePackage"};
        Collectables = FindGameObjectsWithTags(para);
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
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
    // Update is called once per frame
    void Update()
    {
        finishCollect();
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
