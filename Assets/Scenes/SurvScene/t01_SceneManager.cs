using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class t01_SceneManager : MonoBehaviour
{
    //NetworkManager networkManager;

    void Start()
    {
        if (!NetworkDataHolder.IsHost)
        {
            NetworkManager.singleton.networkAddress = NetworkDataHolder.address;
            NetworkManager.singleton.StartClient();
        }
        else
        {
            NetworkManager.singleton.StartHost();
        }
    }

}
