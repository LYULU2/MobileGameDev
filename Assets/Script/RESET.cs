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
    // public GameObject Canvas;
    public GameObject GameManager;
	public GameObject Stats;
    public GameObject Enemy;
    public GameObject TPUnits;

    // Start is called before the first frame update
    public void Reset()
    {
		Stats.GetComponent<StatisticManager>().OnGameReset();
        timer = 0f;
        restartTimes += 1;
        Player.GetComponent<PlayerBehaviour>().Reset();
        // Canvas.GetComponent<CanvasScript>().Reset();
        GameManager.GetComponent<GameManager>().Reset();
        if (Enemy)
        {
            // if the enemy is a single enemy then reset it
            if (Enemy.GetComponent<EnemyMovement>() != null)
            {
                resetEnemy(Enemy.transform);
            }
            else {
                // otherwise enumerate all the children of Enemy and reset them
                foreach (Transform child in Enemy.transform)
                {
                    // check if the child is a single enemy, and reset it if it is
                    if (child.GetComponent<EnemyMovement>() != null)
                    {
                        resetEnemy(child);
                    }
                    else
                    {
                        // otherwise enumerate all the children of the child and reset them
                        foreach (Transform grandchild in child.transform)
                        {
                            Debug.Log(grandchild.name);
                            if (grandchild.GetComponent<EnemyMovement>() != null) {
                                resetEnemy(grandchild);
                            }
                        }
                    }
                    
                }
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies) {
                enemy.SetActive(true);
                Renderer enemyRenderer = enemy.GetComponent<Renderer>();
                if (enemyRenderer != null) {
                    enemyRenderer.material.color = Color.black;
                }
            }
        }
        if (TPUnits)
        {
            // enumerate all the children of TPUnits and reset them
            foreach (Transform child in TPUnits.transform)
            {
                child.GetComponent<teleport>().Reset();
            }
        }
        resetBullet();


    }
    public void resetBullet() {
        // Find all GameObjects with the "blueBullet" tag
        GameObject[] blueBullets = GameObject.FindGameObjectsWithTag("ablueBullet");

        // Loop through all blueBullets and destroy them
        foreach (GameObject bullet in blueBullets) {
            Destroy(bullet);
        }
        
        GameObject[] yellowBullets = GameObject.FindGameObjectsWithTag("ayellowBullet");

        // Loop through all blueBullets and destroy them
        foreach (GameObject bullet in yellowBullets) {
            Destroy(bullet);
        }
        GameObject[] redBullets = GameObject.FindGameObjectsWithTag("aredBullet");

        // Loop through all blueBullets and destroy them
        foreach (GameObject bullet in redBullets) {
            Destroy(bullet);
        }
    }
    void resetEnemy(Transform currentEnemy)
    {
        EnemyMovement flag;
        // Debug.Log(Enemy);
        currentEnemy.TryGetComponent<EnemyMovement>(out flag);
        // Debug.Log(flag);
        if (flag)
        {
            currentEnemy.GetComponent<EnemyMovement>().Reset();
        }
        else
        {
            currentEnemy.GetComponent<WaypointFollower>().Reset();
        }
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
        
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();

        }
        */
        
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
