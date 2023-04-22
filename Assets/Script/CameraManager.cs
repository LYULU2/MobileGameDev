using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;

    public Camera camFollow;
    
    public GameObject instructions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (instructions != null)
            {
                instructions.SetActive(!instructions.activeSelf);
            }
            cam.enabled = !cam.enabled;
            camFollow.enabled = !camFollow.enabled;
        }
        
    }
}
