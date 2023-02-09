using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPress() {
        Debug.Log("we got reset !!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
