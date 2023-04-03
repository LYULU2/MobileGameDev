using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.gameObject.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
