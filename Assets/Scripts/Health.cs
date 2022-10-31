using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviourPun
{
    public Image fillImage;
    public float health = 1;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D collider;

    public void CheckHealth() {
        if (photonView.IsMine && health <= 0) {
            this.GetComponent<PhotonView>().RPC("death",RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void death() {
        rb.gravityScale = 0;
        collider.enabled = false;
        sr.enabled = false;
    
    }

    [PunRPC]
    public void Revive() {
        rb.gravityScale = 1;
        collider.enabled = true;
        sr.enabled = true;
    
    }

    [PunRPC]
    public void HealthUpdate(float damage) {
        fillImage.fillAmount -= damage;
        health = fillImage.fillAmount;
        CheckHealth();
    }

}