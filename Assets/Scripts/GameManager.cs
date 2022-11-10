using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class GameManager : MonoBehaviour
{
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
    private void Awake()
    {
        instance = this;
        canvas.SetActive(true);
    }
    private void Update()
    {
        if (startRespawn) {
            StartRespawn();
        
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

    public void PlayerRelocation() {
        float randomPosition = Random.Range(-5, 5);
        LocalPlayer.transform.localPosition = new Vector2(randomPosition,2);
        
    
    }
    public void EnableRespawn() {

        TimeAmount = 5;
        startRespawn = true;
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
