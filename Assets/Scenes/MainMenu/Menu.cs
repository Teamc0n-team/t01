using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;
public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject UI_Start;

    [SerializeField]
    Animator UI_StartExitAnimation;
    
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
        Debug.Log("::OnStartButton");

        GameObject panel_Foo = GameObject.FindGameObjectWithTag("UI_Start");
        UI_StartExitAnimation = panel_Foo.GetComponent<Animator>();
    }

    private void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    private void DisableStartUI() => UI_Start.SetActive(false);

    public void OnFailed()
    {
        Panel.SetActive(true);
    }

    public void OnHostButton()
    {
        NetworkDataHolder.IsHost = true;
        NetworkDataHolder.NickName = NickNameInput.text;

        UI_StartExitAnimation.Play("Base Layer.StartUIClose");
        Invoke("DisableStartUI", 1.6f);

        t01_NetWorkManager.singleton.StartHost();

        //Invoke("LoadNextScene", 1.7f);
    }

    public void OnConnectButton()
    {
        NetworkDataHolder.IsHost = false;
        NetworkDataHolder.address = AddressInput.text;
        NetworkDataHolder.NickName = NickNameInput.text;

        UI_StartExitAnimation.Play("Base Layer.StartUIClose");
        Invoke("DisableStartUI", 1.6f);

        t01_NetWorkManager.OnDissconnect = OnFailed;
        t01_NetWorkManager.singleton.networkAddress = NetworkDataHolder.address;
        t01_NetWorkManager.singleton.StartClient();
        //Invoke("LoadNextScene", 1.7f);
    }

    private void Update()
    {

    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
