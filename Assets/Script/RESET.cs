using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RESET : MonoBehaviour
{
    public int restartTimes = 0;
    public float timer = 0f;
    // public int SceneIndex;
    public GameObject Player;
    public GameObject Canvas;
    public GameObject GameManager;
	public GameObject Stats;
    // Start is called before the first frame update
    public void Reset()
    {
		Stats.GetComponent<StatisticManager>().OnGameReset();
        timer = 0f;
        restartTimes += 1;
        Player.GetComponent<PlayerBehaviour>().Reset();
        Canvas.GetComponent<CanvasScript>().Reset();
        GameManager.GetComponent<GameManager>().Reset();
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
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();

        }
        
    }
    /*
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Reset");

        if (objs.Length > 1 || SceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
        
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level != SceneIndex)
        {
            Destroy(gameObject);
        }
    }
    */
}
