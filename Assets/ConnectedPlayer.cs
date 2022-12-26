using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectedPlayer : MonoBehaviour
{
    public GameObject CurrentPlayer_Prefab;
    public GameObject CurrentPLayers_GRID;

    public void AddLocalPlayer() {
        GameObject obj = Instantiate(CurrentPlayer_Prefab, new Vector2(0,0), Quaternion.identity);
        obj.transform.SetParent(CurrentPLayers_GRID.transform, false);
        obj.GetComponentInChildren<TMP_Text>().text = "YOU: " + PhotonNetwork.NickName;
        obj.GetComponentInChildren<TMP_Text>().color = Color.green;

    }

    //Called from game manager
    [PunRPC]
    public void UpdatePlayerList(string name) {
        GameObject obj = Instantiate(CurrentPlayer_Prefab, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(CurrentPLayers_GRID.transform, false);
        obj.GetComponentInChildren<TMP_Text>().text = name;
        obj.GetComponentInChildren<TMP_Text>().color = Color.yellow;

            }

    //Called from game manager
    public void RemovePLayerList(string name)
    {
        foreach (TMP_Text playerName in CurrentPLayers_GRID.GetComponentsInChildren<TMP_Text>()) {
            if (name == playerName.text) {
                Destroy(playerName.transform.parent.gameObject);
            }
        
        }
    }
}
