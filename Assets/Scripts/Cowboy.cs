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
    public bool IsGrounded = false;
    public bool DisableInputs = false;
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float jumpForce = 2;

    public string MyName;
    void Awake()
    {
        if (photonView.IsMine)
        {
            GameManager.instance.LocalPlayer = this.gameObject;
            playerCam.SetActive(true);
            playerCam.transform.SetParent(null, false);
            playerName.text = "You : "+PhotonNetwork.NickName;
            playerName.color = Color.green;
            MyName = PhotonNetwork.NickName;
        }
        else {
            playerName.text = photonview.Owner.NickName;
            playerName.color = Color.red;
            
        }
        
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && !DisableInputs) {
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && anim.GetBool("IsMove") == false) {
            shot();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            anim.SetBool("IsShot", false);
            AllowMoving = true;
        
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded) {
            Jump();
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
            bullete.GetComponent<Bullete>().localPlayerObj = this.gameObject;

        }
        else {
            GameObject bullete = PhotonNetwork.Instantiate(BulletePrefab.name, new Vector2(BulleteSpawnPoint1.position.x, BulleteSpawnPoint1.position.y), Quaternion.identity, 0);
            bullete.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered, sprite.flipX);
            bullete.GetComponent<Bullete>().localPlayerObj = this.gameObject;
        }


        
        anim.SetBool("IsShot", true);
        AllowMoving = false;
    
    
    }

    [PunRPC]
    private void FlipSprite_Right() { 
         sprite.flipX = false;
         sprite.transform.localPosition = new Vector2(0, sprite.transform.localPosition.y);
        
    }
    [PunRPC]
    private void FlipSprite_Left()
    {
        sprite.flipX = true;
        sprite.transform.localPosition = new Vector2( -0.85f, sprite.transform.localPosition.y);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }

    void Jump() {
        rb.AddForce(new Vector2(0, jumpForce*Time.deltaTime));
        Debug.Log("Jumped");
    
    
    }
}
