using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForward : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(7.93f, -3.93f);
    }

    // Update is called once per frame
    void Update()
    {
        
        float step = speed * Time.deltaTime;
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
}
