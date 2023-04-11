using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{   
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    private Vector2 stayPos;
    private Vector2 startPos;
    public float distance;
    private float speedEnemy = 3;
    private bool isWaiting = false;
    private float waitingForSeconds = 5;
    private float timer = 0;

    private List<Vector2Int> currentPath;
    private int currentWaypoint;

    void Start()
    {
        startPos = transform.position;
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
        currentPath = new List<Vector2Int>();
        currentWaypoint = 0;
        InvokeRepeating("UpdateRoute", 1.0f, 1.0f);
    }

    
    void Update()
    {

        if (currentPath == null || currentWaypoint >= currentPath.Count)
        {
            return;
        }

        Vector2 targetPosition = currentPath[currentWaypoint];
        Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

        if (Vector2.Distance(transform.position, playerPos.position) < distance && !isWaiting)
        {
            transform.position += (Vector3)moveDirection * speedEnemy * Time.deltaTime;
        }
        else
        {
            startWaiting();
        }

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypoint++;
        }
    }

    void UpdateRoute()
    {
        Vector2Int startTile = GetTileFromWorldPosition(transform.position);
        Vector2Int endTile = GetTileFromWorldPosition(player.transform.position);

        List<Vector2Int> path = FindPath(startTile, endTile);
        if (path != null && path.Count > 0)
        {
            currentPath = path;
            currentWaypoint = 0;
        }
    }

    private Vector2Int GetTileFromWorldPosition(Vector3 position)
    {
        Vector2Int tilePos = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
        return tilePos;
    }

    private List<Vector2Int> FindPath(Vector2Int startTile, Vector2Int endTile)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        Vector2Int currentTile = startTile;
        while (currentTile != endTile)
        {
            if (currentTile.x < endTile.x)
            {
                currentTile.x++;
            }
            else if (currentTile.x > endTile.x)
            {
                currentTile.x--;
            }
            if (currentTile.y < endTile.y)
            {
                currentTile.y++;
            }
            else if (currentTile.y > endTile.y)
            {
                currentTile.y--;
            }

            path.Add(currentTile);
        }

        return path;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            isWaiting = true;
            if (!collision.gameObject.GetComponent<PlayerBehaviour>().protectedByShield)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }

    void startWaiting()
    {
        stayPos = transform.position;
        //print("Start to wait");
        if (timer <= waitingForSeconds)
        {
            timer += Time.deltaTime;;
        }
        else
        {
            print("Waiting complete");
            isWaiting = false; // set the bool to start moving
            timer = 0;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void Reset()
    {
        print("reset");
        transform.position = startPos;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
