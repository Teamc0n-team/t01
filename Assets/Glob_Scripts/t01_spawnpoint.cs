using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class t01_spawnpoint : NetworkBehaviour
{
    public static List<Transform> m_SpawnPoints;
    public static int spawnPointCount = 0;
    
    void Start()
    {
        m_SpawnPoints.Add(transform);
    }



}
