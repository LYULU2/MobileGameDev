using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RESET : MonoBehaviour
{
    public int restartTimes = 0;
    public float timer = 0f;
    // Start is called before the first frame update
    public void Reset()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        restartTimes += 1;
    }
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Reset");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }

    void Start()
    {
        restartTimes = 0; 
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentSceneIndex);
            restartTimes += 1;

        }
        if (Input.GetKeyDown(KeyCode.N) && (currentSceneIndex + 1) < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
    
}
