using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static private InputManager m_pInstance;
    static public InputManager Instance
    {
        get { return m_pInstance; }
    }

    InputManager()
    {
        m_pInstance = this;
        _OnPlayerUpdate = _OnPlayerUpdate_empty;
    }

    private delegate void OnPlayerUpdate();
    private OnPlayerUpdate _OnPlayerUpdate;

    [SerializeField]
    Vector2 m_MousePos;
    
    [SerializeField]
    t01_player pl;

    [SerializeField]
    Vector2 m_Move;

    public void SetupPlayer(t01_player pl)
    {
        this.pl = pl;
        _OnPlayerUpdate = _OnPlayerUpdate_work;
    }

    void _OnPlayerUpdate_work()
    {
        CameraManager.Instance.T01_Update(m_MousePos);
        pl.CmdUpdatePosition(m_Move);
    }

    void _OnPlayerUpdate_empty()
    {
        
    }

    private void Update()
    {
        ProcessInput();

        _OnPlayerUpdate();
    }

    private void FixedUpdate()
    {
        
    }

    void ProcessInput()
    {
        m_MousePos.x = Input.GetAxisRaw("Mouse X");
        m_MousePos.y = Input.GetAxisRaw("Mouse Y");

        m_Move.x = Input.GetAxis("Horizontal");
        m_Move.y = Input.GetAxis("Vertical");
    }

}
