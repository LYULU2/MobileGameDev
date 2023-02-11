using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Reset Button");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }
    private void OnLevelWasLoaded(int level)
    {
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }
}
