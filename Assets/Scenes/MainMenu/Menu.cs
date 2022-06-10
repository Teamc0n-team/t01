using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject UI_Start;

    [SerializeField]
    Animation UI_StartExitAnimation;
    
    [SerializeField]
    InputField NickNameInput;

    [SerializeField]
    InputField AddressInput;

    [SerializeField]
    GameObject Panel;

    bool IsUI_Start = false;

    public void OnStartButton()
    {
        IsUI_Start = !IsUI_Start;
        Panel.SetActive(false);
        UI_Start.SetActive(true);
        //Panel.SetActive(true);
        Debug.Log("::OnStartButton");
        //ColorBlock m_NewBlock = new ColorBlock();
        //m_NewBlock.normalColor = Color.black;
        //StartButton.colors = m_NewBlock;
    }

    private void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void OnHostButton()
    {
        NetworkDataHolder.IsHost = true;
        NetworkDataHolder.NickName = NickNameInput.text;

        UI_StartExitAnimation.();
        Invoke("LoadNextScene", 4.6f);
    }

    public void OnConnectButton()
    {
        NetworkDataHolder.IsHost = false;
        NetworkDataHolder.address = AddressInput.text;
        NetworkDataHolder.NickName = NickNameInput.text;

        UI_StartExitAnimation.Play();
        Invoke("LoadNextScene", 4.6f);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
