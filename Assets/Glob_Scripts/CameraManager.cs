using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    static private CameraManager m_pInstance;
    static public CameraManager Instance
    {
        get { return m_pInstance; }
    }

    CameraManager()
    {
        m_pInstance = this;
        OnPlayerUpdate = DelegateOnPlayerUpdate_Empty;
    }

    public float SensX;
    public float SensY;

    [SerializeField]
    Vector2 Rotation;

    [SerializeField]
    Vector2 _MousePos;

    t01_player pl;

    void Start()
    {

        Rotation = new Vector2(0, 0);
    }

    private delegate void DelegateOnPlayerUpdate(float y);
    private DelegateOnPlayerUpdate OnPlayerUpdate;

    public void SetupPlayer(t01_player pl)
    {
        this.pl = pl;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OnPlayerUpdate = DelegateOnPlayerUpdate_Work;
    }

    public void T01_Update(Vector2 MousePos)
    {
        if (pl == null) return;
        _MousePos.x = MousePos.x * Time.deltaTime * SensX;
        _MousePos.y = MousePos.y * Time.deltaTime * SensY;

        Rotation.y += MousePos.x;

        Rotation.x -= MousePos.y;
        Rotation.x = Mathf.Clamp(Rotation.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(Rotation.x, Rotation.y, 0f);

        OnPlayerUpdate(Rotation.y);
    }

    void DelegateOnPlayerUpdate_Empty(float y)
    {
    }

    void DelegateOnPlayerUpdate_Work(float y)
    {
        pl.CmdUpdateOrientation(Rotation.y);
        transform.position = pl.transform.position;
    }

}
