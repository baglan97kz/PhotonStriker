using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullete : MonoBehaviourPun
{
    public float MoveSpeed = 8;
    public float DestroyTime = 2f;

    IEnumerator destroyBullete() { 
        yield return new WaitForSeconds(DestroyTime);
        this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
    
    }

    [PunRPC]
    void Destroy()
    {
        Destroy(this.gameObject);
     
    }
}
