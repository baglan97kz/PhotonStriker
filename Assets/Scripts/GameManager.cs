using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject cpCanvas;
    public ConnectedPlayer cp;

    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject sceneCam;

    public TMPro.TMP_Text spawnTimer;
    public GameObject respawnUI;

    private float TimeAmount = 5;
    private bool startRespawn;
    public TMPro.TMP_Text pingrate;

    [HideInInspector]
    public GameObject LocalPlayer;
    public static GameManager instance = null;

    public GameObject LeaveScreen;

    public TimeOut timeOut_obj;

    public GameObject feedbox;
    public GameObject feedText_prefab;

    public GameObject KillGotKilledFeedBox;
    //public GameObject KilledFeedText;
    private void Awake()
    {
        instance = this;
        canvas.SetActive(true);
    }

    private void Start()
    {
        cp.AddLocalPlayer();
        cp.GetComponent<PhotonView>().RPC("UpdatePlayerList", RpcTarget.OthersBuffered, PhotonNetwork.NickName) ;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleLeaveScreen();
        }
        if (startRespawn) {
            StartRespawn();
        
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            cpCanvas.SetActive(true);
        }
        else {
            cpCanvas.SetActive(false);
        }

        pingrate.text = "NetworkPing : " + PhotonNetwork.GetPing();
    }

    public void StartRespawn() {
        TimeAmount -= Time.deltaTime;
        spawnTimer.text ="Respawn in : " + TimeAmount.ToString("F0");
        if (TimeAmount <= 0) { 
            respawnUI.SetActive(false);
            startRespawn = false;
            PlayerRelocation();
            LocalPlayer.GetComponent<Health>().EnableInputs();
            LocalPlayer.GetComponent<PhotonView>().RPC("Revive",RpcTarget.AllBuffered);
}
    }

    public void ToggleLeaveScreen() {
        if (LeaveScreen.activeSelf)
        {
            LeaveScreen.SetActive(false);
        }
        else { 
            LeaveScreen.SetActive(true);    
        }

    
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject tempObj =  Instantiate(feedText_prefab, new Vector2(0f,0f), Quaternion.identity);
        tempObj.transform.SetParent(feedbox.transform);
        tempObj.transform.localScale = Vector3.one;
        tempObj.GetComponent<TMP_Text>().text = newPlayer.NickName + " has joined the game";
        Destroy(tempObj,3);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        cp.RemovePLayerList(otherPlayer.NickName);
        GameObject tempObj = Instantiate(feedText_prefab, new Vector2(0f, 0f), Quaternion.identity);
        tempObj.transform.SetParent(feedbox.transform);
        tempObj.transform.localScale = Vector3.one;
        tempObj.GetComponent<TMP_Text>().text = otherPlayer.NickName + " has left the game";
        Destroy(tempObj, 3);
    }


    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    
    }

    public void PlayerRelocation() {
        float randomPosition = Random.Range(-5, 5);
        LocalPlayer.transform.localPosition = new Vector2(randomPosition,2);
        
    
    }
    public void EnableRespawn() {

        TimeAmount = 5;
        startRespawn = true;
        if(!timeOut_obj.TimeOutUI.activeSelf)
            respawnUI.SetActive(true);
    }

    public void SpawnPlayer() {
        float randomValue = Random.Range(-5,5);
        PhotonNetwork.Instantiate(playerPrefab.name,
            new Vector2(playerPrefab.transform.position.x * randomValue, 
            playerPrefab.transform.position.y), Quaternion.identity,0);
        canvas.SetActive(false);
        sceneCam.SetActive(false);
        
    
    }

    
}
