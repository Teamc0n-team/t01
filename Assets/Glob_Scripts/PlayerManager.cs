using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private int m_nPlayersCount;
    
    [SerializeField]
    public int nPlayersCount
    {
        get { return m_nPlayersCount; }
    }

    static private PlayerManager m_Instance;
    static public PlayerManager SingleTone
    {
        get { return m_Instance; }
    }

    PlayerManager()
    {
        m_Instance = this;
    }

    [SerializeField]
    List<t01_player> m_LPlayers;
    private void Start()
    {
        m_LPlayers = new List<t01_player>();
    }

    [Command]
    public void CmdAddPlayer(t01_player pl)
    {
        m_LPlayers.Add(pl);
    }

    [Command]
    public void CmdGetPlayersCount()
    {
        RpcSetPlayersCount(m_LPlayers.Count);
    }
    
    [ClientRpc]
    void RpcSetPlayersCount(int nCount)
    {
        m_nPlayersCount = nCount;
    }

}
