using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    //private Collider2D[] Surroundings;
    private int movementSpeed;
    private new Rigidbody2D rigidbody;
    private float movementHorizontal;
    private float movementVertical;

    // Start is called before the first frame update
    void Start()
    {
        //Surroundings = CollisionList(); //Move by block
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movementHorizontal = Input.GetAxisRaw("Horizontal");
        movementVertical = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if (movementHorizontal != 0)
        {
            Debug.Log("horray");
            rigidbody.velocity = new Vector2(movementSpeed * movementHorizontal, 0);
        }
        else if (movementVertical != 0)
        {
            rigidbody.velocity = new Vector2(0, movementSpeed * movementVertical);
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
    // Update is called once per frame
    /*
     * Move by block thing
    void Update()
    {

        movementHorizontal = Input.GetButtonDown("Horizontal");
        movementVertical = Input.GetButtonDown("Vertical");
        if (movementHorizontal)
        {
            Surroundings = CollisionList();//Move by block
            float direction = Input.GetAxisRaw("Horizontal");
            Vector3 target = new Vector3(gameObject.transform.position.x + direction, gameObject.transform.position.y, gameObject.transform.position.z);
            for (int i = 0; i < 4; i++)
            {
                if (Surroundings[i] != null && Surroundings[i].gameObject.transform.position == target)
                {
                    rigidbody.MovePosition(new Vector2(target.x, target.y));
                    break;
                }
            }
            return;
        }
        if (movementVertical)
        {
            Debug.Log("Ver");
            Surroundings = CollisionList();//Move by block
            float direction = Input.GetAxisRaw("Vertical");
            Vector3 target = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + direction, gameObject.transform.position.z);
            for (int i = 0; i < 4; i++)
            {
                if (Surroundings[i] != null && Surroundings[i].gameObject.transform.position == target)
                {
                    rigidbody.MovePosition(new Vector2(target.x, target.y));
                    break;
                }
            }
            return;
        }
    }
    /*
     * Move by block
    private Collider2D[] CollisionList()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, 1);
        Collider2D[] outputArray = new Collider2D[4];
        int idx = 0;
        foreach (Collider2D collider in colliderArray)
        {
            if (collider.tag != "Player" &&
                collider.tag != "DeadWalls" &&
                (collider.gameObject.transform.position.x == gameObject.transform.position.x || collider.gameObject.transform.position.y == gameObject.transform.position.y))
            {
                outputArray[idx] = collider;
                idx++;
            }
        }
        return outputArray;
    }
    */
}
