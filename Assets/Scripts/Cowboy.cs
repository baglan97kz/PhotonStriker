using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;

public class Cowboy : MonoBehaviourPun
{
    public float MoveSpeed =5;
    public GameObject playerCam;
    public SpriteRenderer sprite;
    public PhotonView photonview;
    public Animator anim;
    private bool AllowMoving = true;

    public GameObject BulletePrefab;
    public Transform BulleteSpawnPoint1, BulleteSpawnPoint2;

    public TMP_Text playerName;
    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine)
        {
            playerCam.SetActive(true);
            playerName.text = "You : "+PhotonNetwork.NickName;
            playerName.color = Color.green;
        }
        else {
            playerName.text = photonview.Owner.NickName;
            playerName.color = Color.red;
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
        if (AllowMoving)
        {
            var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
            transform.position += movement * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightControl) && anim.GetBool("IsMove") == false) {
            shot();
        }
        else if (Input.GetKeyUp(KeyCode.RightControl)) {
            anim.SetBool("IsShot", false);
            AllowMoving = true;
        
        }


        if (Input.GetAxisRaw("Horizontal") == -1 && anim.GetBool("IsShot") == false) {
            photonView.RPC("FlipSprite_Left", RpcTarget.AllBuffered);
            
            anim.SetBool("IsMove", true);
        } 
        else if (Input.GetAxisRaw("Horizontal") == 1 && anim.GetBool("IsShot") == false) {
            photonView.RPC("FlipSprite_Right", RpcTarget.AllBuffered);

            anim.SetBool("IsMove", true);

        }
        else{
            anim.SetBool("IsMove", false);

        }
    }

    private void shot() {

        if (sprite.flipX)
        {
            GameObject bullete = PhotonNetwork.Instantiate(BulletePrefab.name, new Vector2(BulleteSpawnPoint2.position.x, BulleteSpawnPoint2.position.y), Quaternion.identity, 0);
            bullete.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered, sprite.flipX);

        }
        else {
            GameObject bullete = PhotonNetwork.Instantiate(BulletePrefab.name, new Vector2(BulleteSpawnPoint1.position.x, BulleteSpawnPoint1.position.y), Quaternion.identity, 0);
            bullete.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered, sprite.flipX);
        }


        
        anim.SetBool("IsShot", true);
        AllowMoving = false;
    
    
    }

    [PunRPC]
    private void FlipSprite_Right() { 
         sprite.flipX = false;

    }
    [PunRPC]
    private void FlipSprite_Left()
    {
        sprite.flipX = true;

    }
}
