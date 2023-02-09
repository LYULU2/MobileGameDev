using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject[] Collectables;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
