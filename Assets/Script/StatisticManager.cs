using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Proyecto26;
using Model; // Data class's namespace
using UnityEngine.SceneManagement;

public class StatisticManager : MonoBehaviour
{
    public GameObject player;

    private readonly string basePath = "https://qaqthebest-3d6c6-default-rtdb.firebaseio.com/";
    private RequestHelper currentRequest;
    /*
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Stat");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    */
    /**
     * Post win/lose data to the database
     */
    public void PostData()
    {
        currentRequest = new RequestHelper {
            Uri = basePath + "/midtermFull.json",
            Body = new Data {
                curDistance = player.GetComponent<PlayerBehaviour>().totalDistance,
                resetTimes = GameObject.FindGameObjectsWithTag("Reset")[0].GetComponent<RESET>().restartTimes,
                playTime = GameObject.FindGameObjectsWithTag("Reset")[0].GetComponent<RESET>().timer,
                collectBlue = player.GetComponent<PlayerBehaviour>().Blue,
                collectYellow = player.GetComponent<PlayerBehaviour>().Yellow,
                collectRed = player.GetComponent<PlayerBehaviour>().Red,
                collectReducePackage = player.GetComponent<PlayerBehaviour>().reducePackage,
                numHitEnemy = player.GetComponent<PlayerBehaviour>().numHitEnemy,
                numBounceEnemy = player.GetComponent<PlayerBehaviour>().numBounceEnemy,
                sceneName = SceneManager.GetActiveScene().name,
                portalLockHit = player.GetComponent<PlayerBehaviour>().portalLockHit
            },
            EnableDebug = true
        };
        RestClient.Post<Data>(currentRequest)
            .Then(res => {

                // And later we can clear the default query string params for all requests
                RestClient.ClearDefaultParams();

                Debug.Log(JsonUtility.ToJson(res, true));
            })
            .Catch(err => Debug.Log(err.Message));
    }
    
    /**
     * Post reset data to the database
     */
    public void PostResetData()
    {
        currentRequest = new RequestHelper {
            Uri = basePath + "/resetMidterm.json",
            Body = new ResetData {
                curDistance = player.GetComponent<PlayerBehaviour>().totalDistance,
                playTime = GameObject.FindGameObjectsWithTag("Reset")[0].GetComponent<RESET>().timer,
                collectBlue = player.GetComponent<PlayerBehaviour>().Blue,
                collectYellow = player.GetComponent<PlayerBehaviour>().Yellow,
                collectRed = player.GetComponent<PlayerBehaviour>().Red,
                collectReducePackage = player.GetComponent<PlayerBehaviour>().reducePackage,
                numHitEnemy = player.GetComponent<PlayerBehaviour>().numHitEnemy,
                numBounceEnemy = player.GetComponent<PlayerBehaviour>().numBounceEnemy,
                sceneName = SceneManager.GetActiveScene().name
            },
            EnableDebug = true
        };
        RestClient.Post<ResetData>(currentRequest)
            .Then(res => {

                // And later we can clear the default query string params for all requests
                RestClient.ClearDefaultParams();

                Debug.Log(JsonUtility.ToJson(res, true));
            })
            .Catch(err => Debug.Log(err.Message));
    }
    
    /**
     * Post tutorial data to the database
     */
    public void PostTutorialData(int finish)
    {
        currentRequest = new RequestHelper {
            Uri = basePath + "/tutorial.json",
            Body = new TutorialData {
                sceneName = SceneManager.GetActiveScene().name,
                curDistance = player.GetComponent<PlayerBehaviour>().totalDistance,
                playTime = GameObject.FindGameObjectsWithTag("Reset")[0].GetComponent<RESET>().timer,
                finishStatus = finish
            },
            EnableDebug = true
        };
        RestClient.Post<TutorialData>(currentRequest)
            .Then(res => {

                // And later we can clear the default query string params for all requests
                RestClient.ClearDefaultParams();

                Debug.Log(JsonUtility.ToJson(res, true));
            })
            .Catch(err => Debug.Log(err.Message));
    }

    public void OnGameFinish()
    {
        //post data when not testing in editor
#if !UNITY_EDITOR
        PostData();
#endif
    }
    public void OnGameReset()
    {
        //post data when not testing in editor
#if !UNITY_EDITOR
        PostResetData();
#endif
    }
    public void OnGameFinishTutorial(int finish)
    {
        //post data when not testing in editor
#if !UNITY_EDITOR
        PostTutorialData(finish);
#endif
    }
}
