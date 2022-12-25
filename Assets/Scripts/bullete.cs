using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullete : MonoBehaviourPun
{
    public bool MovingDir;
    public float MoveSpeed = 8;
    public float DestroyTime = 2f;
    public float bulleteDamage = 0.3f;

    IEnumerator destroyBullete() { 
        yield return new WaitForSeconds(DestroyTime);
        this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
    
    }

    private void Update()
    {
        if (!MovingDir)
        {
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
        else {
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);


        }
    }

    [PunRPC]
    public void ChangeDirection(bool a) {

        MovingDir = a;
    
    }
    
    [PunRPC]
    void Destroy()
    {
        Destroy(this.gameObject);
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) {
            return;
        
        }

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null && (!target.IsMine || target.IsRoomView)) {
            if (target.tag == "Player") {
                target.RPC("HealthUpdate", RpcTarget.AllBuffered, bulleteDamage);
                target.GetComponent<HurtEffect>().GotHit();
            }

            this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
            
        
        }  
        
    }
}
