using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image fillImage;

    [PunRPC]
    public void HealthUpdate(float damage) {
        fillImage.fillAmount -= damage; 
    }

}