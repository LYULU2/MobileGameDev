using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    private Vector2 stayPos;
    public float distance;
    public float speedEnemy;
    private bool isWaiting = false;
    public float waitingForSeconds = 3;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) < distance && !isWaiting)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        }
        else
        {
            startWaiting();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            isWaiting = true;
        }
    }

    void startWaiting()
    {
        stayPos = transform.position;
        print("Start to wait");
        if (timer <= waitingForSeconds)
        {
            timer += Time.deltaTime;;
        }
        else
        {
            print("Waiting complete");
            isWaiting = false; // set the bool to start moving
            timer = 0;
        }
    }

}
