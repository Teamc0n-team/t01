using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class t01_player : NetworkBehaviour
{
    [SerializeField]
    private Transform orientation;

    [SerializeField]
    bool IsGrounded = false;

    [SyncVar]
    [SerializeField]
    float Speed;

    [SyncVar]
    public string m_sNickName;

    [SyncVar]
    [SerializeField]
    float JumpForce;

    [SerializeField]
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isClient)
        {
            CmdSendNickName(NetworkDataHolder.NickName);
        }
        if (isClient && isLocalPlayer)
        {
            CameraManager.Instance.SetupPlayer(this);
            InputManager.Instance.SetupPlayer(this);
        }

        IsGrounded = true;

        if (isServer)
        {
            Speed = 4f;

            JumpForce = 300f;
        }
    }

    void CmdSendNickName(string Name)
    {
        m_sNickName = Name;
        RpcSetPlayerName(Name);
    }

    [ClientRpc]
    void RpcSetPlayerName(string s)
    {
        GetComponentInChildren<TextMesh>().text = s;
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

    public void CmdIsGroundedUpate(string tag, bool State)
    {
        if (tag == "Ground")
        {
            IsGrounded = State;
        }
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

    [Command]
    public void CmdJump()
    {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Acceleration);
    }
}

