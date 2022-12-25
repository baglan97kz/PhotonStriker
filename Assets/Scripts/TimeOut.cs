using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class TimeOut : MonoBehaviourPun
{
    private float idleTime = 30f;
    private float timer = 10;

    public GameObject TimeOutUI;
    public TMP_Text TimeOutUI_Text;

    private bool TimeOver = false;

    
    void Update()
    {

       
        
            if (!TimeOver )
            {
                if (Input.anyKey || GameManager.instance.respawnUI.activeSelf)
                {
                    idleTime = 30;
                }
                idleTime -= Time.deltaTime;

                if (idleTime <= 0)
                {
                    playerNotMoving();
                }

                if (TimeOutUI.activeSelf )
                {
                    timer -= Time.deltaTime;
                    TimeOutUI_Text.text = "Disconnecting in: " + timer.ToString("F0");

                    if (timer <= 0)
                    {
                        TimeOver = true;
                    }
                    else if ((timer > 0 && Input.anyKey) || GameManager.instance.respawnUI.activeSelf)
                    {
                        idleTime = 30;
                        timer = 10;
                        TimeOutUI.SetActive(false);

                    }

                }
            }
            else
            {
                leaveGame();

            }
       
    }

    void playerNotMoving() {
        TimeOutUI.SetActive(true);    
    }

    void leaveGame() {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    
    }
}
