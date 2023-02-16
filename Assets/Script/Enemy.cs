using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float range = 100f;
    public GameObject player;
    public Vector2 StartPosition;
    public float speed = 3.0f;
    private bool isWaiting = false;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
    }
     
    // Update is called once per frame
    void Update()
    {
        // when out of range, the enemy slowly move back to the origin
        if (Vector2.Distance(player.transform.position , transform.position) > range)
        {
            transform.position = Vector2.MoveTowards(transform.position, StartPosition, Time.deltaTime * speed);
        }
        if (!isWaiting)
        {
            if (Vector2.Distance(player.transform.position, transform.position) == 0)
            {
                Debug.Log("collision");
                StartCoroutine(Wait());
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isWaiting && collision.transform.tag == "Player")
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        isWaiting = true;  //set the bool to stop moving
        print("Start to wait");
        yield return new WaitForSeconds(3); // wait for 5 sec
        print("Wait complete");
        isWaiting = false; // set the bool to start moving

    }
}
