using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject[] Collectables;
    private resetButton rb = new resetButton();
    private GameObject player;
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
        string[] para = new string[2] {"Blue","Yellow"};
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
            
            Color playerColor = player.GetComponent<SpriteRenderer>().color;
            Color finisher = GameObject.FindGameObjectsWithTag("Finish")[0].GetComponent<SpriteRenderer>().color;
            string playerColorHex = ColorUtility.ToHtmlStringRGBA(playerColor);
            string wallColorHex = ColorUtility.ToHtmlStringRGBA(finisher);
            if (playerColorHex != wallColorHex) rb.OnButtonPress();
        }
    }
    // Update is called once per frame
    void Update()
    {
        finishCollect();
    }
}
