using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class t01_SceneManager : MonoBehaviour
{
    void OnDissconnect()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
    }

    void Start()
    {
        t01_NetWorkManager.OnDissconnect = OnDissconnect;

        //t01_NetWorkManager.singleton.networkAddress = NetworkDataHolder.address;
        //if (NetworkDataHolder.IsHost)
        //{
        //    t01_NetWorkManager.singleton.StartHost();
        //}
       // else
        //{
         //   t01_NetWorkManager.singleton.StartClient();
        //}
    }

}
