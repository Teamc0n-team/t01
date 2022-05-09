using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NickNameController : MonoBehaviour
{
    [SerializeField]
    TMP_Text m_Text;
    [SerializeField]
    Camera m_Camera;

    void Start()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 RenderPos = m_Camera.transform.position;

        m_Text.transform.LookAt(RenderPos);
        m_Text.transform.transform.Rotate(new Vector3(0, 180, 0));
    }
}
