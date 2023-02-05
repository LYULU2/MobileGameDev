using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Proyecto26;
using Model; // Data class's namespace

public class StatisticManager : MonoBehaviour
{
    private readonly string basePath = "https://qaqthebest-3d6c6-default-rtdb.firebaseio.com/";
    private RequestHelper currentRequest;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PostData()
    {
        currentRequest = new RequestHelper {
            Uri = basePath + "/posts.json",
            Body = new Data {
                curDistance = player.GetComponent<PlayerBehaviour>().curDistance
            },
            EnableDebug = true
        };
        RestClient.Post<Data>(currentRequest)
            .Then(res => {

                // And later we can clear the default query string params for all requests
                RestClient.ClearDefaultParams();

                Debug.Log( JsonUtility.ToJson(res, true));
            })
            .Catch(err => Debug.Log( err.Message));
    }

    public void OnGameFinish()
    {
        PostData();
    }
}
