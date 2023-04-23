using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunColorBehave : MonoBehaviour
{
    private PlayerBehaviour pb;
	[SerializeField] 
	private Sprite defaultGunSprite;
	[SerializeField] 
	private Sprite yellowGunSprite;
	[SerializeField] 
	private Sprite redGunSprite;
	[SerializeField] 
	private Sprite blueGunSprite;
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
			this.GetComponent<SpriteRenderer>().sprite = defaultGunSprite;
            // use the default black sprite
            Debug.Log("Black gun");
        }
        else
        {
            int c = queue.Peek();
            if (c == 0)
            {
                //blue 
				this.GetComponent<SpriteRenderer>().sprite = blueGunSprite;
                Debug.Log("Blue gun");
            }
            else if (c == 1)
            {
                //yellow
				this.GetComponent<SpriteRenderer>().sprite = yellowGunSprite;
                Debug.Log("yellow gun");
            }
            else if (c == 2)
            {
                //red
				this.GetComponent<SpriteRenderer>().sprite = redGunSprite;
                Debug.Log("red gun");
            }
        }

    }
}
