using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Cowboy : MonoBehaviourPun
{
    public float MoveSpeed =5;
    public GameObject playerCam;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine)
        {
            playerCam.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) {
            checkInputs();
        
        }
        
    }

    private void checkInputs()
    {
        var movement = new Vector3(Input.GetAxisRaw("Horizontal"),0);
        transform.position += movement * MoveSpeed * Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") == -1) {
            sprite.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1) {
            sprite.flipX = false;        
        }
    }
}
