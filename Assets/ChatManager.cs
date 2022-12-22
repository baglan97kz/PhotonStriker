using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour, IPunObservable
{
    public PhotonView photonView;
    public GameObject BubbleSpeech;
    public TMP_Text ChatText;

    public Cowboy player;

    TMP_InputField ChatInput;
    private bool DisableSend;

    private void Awake()
    {
        ChatInput = GameObject.Find("ChatInputField").GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (photonView.IsMine) {
            if (ChatInput.isFocused)
            {
                player.DisableInputs = true;
            }
            else {
                player.DisableInputs = false;
            }
        
        }

        if (!DisableSend && ChatInput.isFocused) {
            if (ChatInput.text != "" && ChatInput.text.Length > 1 && Input.GetKeyDown(KeyCode.Space)) {
                BubbleSpeech.SetActive(true);
                photonView.RPC("SendMsg", RpcTarget.AllBuffered, ChatInput.text);
                ChatInput.text = "";
                DisableSend = true;
            }
        }
    }

    [PunRPC]
    void SendMsg(string msg) {
        ChatText.text = msg;
        StartCoroutine(hideBubbleSpeech());
    
    }

    IEnumerator hideBubbleSpeech() {
        yield return new WaitForSeconds(3);
        BubbleSpeech.SetActive(false);
        DisableSend = false;
    
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(BubbleSpeech.activeSelf);
        }
        else if (stream.IsReading) {
            BubbleSpeech.SetActive((bool)stream.ReceiveNext());
        }
    }
}
