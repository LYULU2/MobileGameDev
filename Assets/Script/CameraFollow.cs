using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform);
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -8);
    }
}
