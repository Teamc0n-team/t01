using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System;

public class t01_NetWorkManager : NetworkManager
{

    public static Action OnDissconnect;
    //public static int nSceneToLoad;

    //public override void OnStartServer()
    //{
    //}

    //public override void OnStartClient()
    //{
//
    //}

    public override void OnClientConnect(NetworkConnection conn)
    {
       base.OnClientConnect(conn);

       Instantiate(base.playerPrefab, t01_spawnpoint.m_SpawnPoints[t01_spawnpoint.spawnPointCount]);
       t01_spawnpoint.spawnPointCount++;
    }

    //public override void OnServerConnect(NetworkConnectionToClient conn)
    //{
     //   base.OnServerConnect(conn);
    //}

    //public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    //{
     //   base.OnServerAddPlayer(conn);
    //}

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        OnDissconnect?.Invoke();
        //SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
    }

    public override void OnClientError(Exception exception)
    {
        base.OnClientError(exception);
        Debug.Log(exception.Message);
    }

}
