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
    public GameObject playerCanvas;

    public Cowboy playerScript;

    public void CheckHealth() {
        if (photonView.IsMine && health <= 0) {
            GameManager.instance.EnableRespawn();
            playerScript.DisableInputs = true;
            this.GetComponent<PhotonView>().RPC("death",RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void death() {
        rb.gravityScale = 0;
        collider.enabled = false;
        sr.enabled = false;
        playerCanvas.SetActive(false);
    
    }

    public void EnableInputs()
    {
        playerScript.DisableInputs = false;

    }

    [PunRPC]
    public void Revive() {
        rb.gravityScale = 1;
        collider.enabled = true;
        sr.enabled = true;
        playerCanvas.SetActive(true);

        fillImage.fillAmount = 1;
        health = 1;

    }

    [PunRPC] 
    public void HealthUpdate(float damage) {
        fillImage.fillAmount -= damage;
        health = fillImage.fillAmount;
        CheckHealth();
    }

}