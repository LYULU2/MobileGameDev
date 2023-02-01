using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int movementSpeed;
    private new Rigidbody2D rigidbody;
    private float movementHorizontal;
    private float movementVertical;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementHorizontal = Input.GetAxisRaw("Horizontal");
        movementVertical = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if (movementHorizontal != 0)
        {
            rigidbody.velocity = new Vector2(movementSpeed * movementHorizontal, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            if (movementVertical != 0)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, movementSpeed * movementVertical);
            }
            else
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            }
        }
        
    }
}
