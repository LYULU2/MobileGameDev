using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RESET : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentSceneIndex);
        }
        if (Input.GetKeyDown(KeyCode.N) && (currentSceneIndex + 1) < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
