using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject UserNameScreen, ConnectScreen;

    [SerializeField]
    private GameObject CreateUserNameButton;
    [SerializeField]
    private TMP_InputField UserNameInput, CreateRoomInput, JoinRoomInput;

    void Awake() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby(TypedLobby.Default);

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby!!!!");
        UserNameScreen.SetActive(true);
    }

    #region UIMethods
    public void OnClick_CreateNameBtn() {

        PhotonNetwork.NickName = UserNameInput.text;
        UserNameScreen.SetActive(false);
        ConnectScreen.SetActive(true);
    
    }

    public void OnNameField_Changed() {
        if (UserNameInput.text.Length >= 2)
        {
            CreateUserNameButton.SetActive(true);
        }
        else {
            CreateUserNameButton.SetActive(false);
        }
    
         
    }
    #endregion


}
