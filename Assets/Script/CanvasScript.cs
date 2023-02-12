using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanvasScript : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;

    public void Reset()
    {
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }
}
