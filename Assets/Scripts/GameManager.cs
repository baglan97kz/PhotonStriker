using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject sceneCam;

    public TMPro.TMP_Text pingrate;
    private void Awake()
    {
        canvas.SetActive(true);
    }
    private void Update()
    {
        pingrate.text = "NetworkPing : " + PhotonNetwork.GetPing();
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
