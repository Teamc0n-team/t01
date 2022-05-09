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
    InputField NickNameInput;

    [SerializeField]
    InputField AddressInput;

    public void OnStartButton()
    {
        UI_Start.SetActive(true);
    }

    public void OnHostButton()
    {
        NetworkDataHolder.IsHost = true;
        NetworkDataHolder.NickName = NickNameInput.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnConnectButton()
    {
        NetworkDataHolder.IsHost = false;
        NetworkDataHolder.address = AddressInput.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
