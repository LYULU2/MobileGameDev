using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerUpLight : MonoBehaviour
{
    [SerializeField] private Sprite lightSprite;
    [SerializeField] private Sprite dimSprite;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void updateUXLight(bool powerup)
    {
        if (powerup)
        {
            this.GetComponent<Image>().sprite = lightSprite;
        }
        else
        {
            this.GetComponent<Image>().sprite = dimSprite;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
