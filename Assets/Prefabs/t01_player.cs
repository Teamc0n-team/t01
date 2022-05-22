using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

public class t01_player : NetworkBehaviour
{

    [SerializeField]
    private Transform orientation;

    [SyncVar]
    [SerializeField]
    bool IsGrounded = false;

    [SyncVar]
    [SerializeField]
    float Speed;

    [SyncVar]
    [SerializeField]
    public string m_sNickName;

    [SyncVar]
    [SerializeField]
    float JumpForce;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    TMPro.TMP_Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isServer)
        {
            IsGrounded = false;
            Speed = 4f;
            JumpForce = 300f;
        }

        if (isClient && isLocalPlayer)
        {
            CameraManager.Instance.SetupPlayer(this);
            InputManager.Instance.SetupPlayer(this);
            
            CmdSendNickName(NetworkDataHolder.NickName);
        }
    }

    [Command]
    void CmdSendNickName(string Name)
    {
        m_sNickName = Name;
        m_Text.text = Name;
        RpcSetPlayerName(Name);
    }

    [ClientRpc]
    void RpcSetPlayerName(string s)
    {
        m_Text.text = s;
        name = s;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (IsGrounded)
            {
                CmdJump();
                IsGrounded = false;
                Debug.Log("jump");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        CmdIsGroundedUpate(collision.gameObject.tag, true);
    }

    void OnCollisionExit(Collision collision)
    {
       CmdIsGroundedUpate(collision.gameObject.tag, false);
    }

    [Command]
    public void CmdIsGroundedUpate(string tag, bool State)
    {
        if (tag == "Ground")
        {
            IsGrounded = State;
        }
    }

    public void FixedUpdate()
    {
        m_Text.text = m_sNickName;
    }

    [Command]
    public void CmdUpdateOrientation(float y)
    {
        orientation.rotation = Quaternion.Euler(0f, y, 0f);
        transform.rotation = orientation.rotation;
    }
    
    [Command]
    public void CmdUpdatePosition(Vector2 MovmentVector)
    {
        Vector3 movement = new Vector3(MovmentVector.x, 0.0f, MovmentVector.y);
        transform.Translate(movement * Speed * Time.deltaTime);
    }

    //[Command]
    public void CmdJump()
    {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Acceleration);
    }
}

