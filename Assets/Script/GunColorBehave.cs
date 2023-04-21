using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunColorBehave : MonoBehaviour
{
    private PlayerBehaviour pb;
    // Start is called before the first frame update
    void Start()
    {
        pb = this.GetComponentInParent<PlayerBehaviour>(); //assign player
    }

    // Update is called once per frame
    void Update()
    {
        //update gun color
        Queue<int> queue = pb.getColorQueue();
        if (queue.Count == 0)
        {
            // use the default black sprite
            Debug.Log("Black gun");
        }
        else
        {
            int c = queue.Peek();
            if (c == 0)
            {
                //blue 
                Debug.Log("Blue gun");
            }
            else if (c == 1)
            {
                //yellow
                Debug.Log("yellow gun");
            }
            else if (c == 2)
            {
                //red
                Debug.Log("red gun");
            }
        }

    }
}
