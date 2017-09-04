using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMP : Photon.PunBehaviour
{
    Camera playerCam;

    void Awake()
    {
       // DontDestroyOnLoad(gameObject);
        playerCam = GetComponentInChildren<Camera>();

        if (!photonView.isMine)
        {
            playerCam.gameObject.SetActive(false);
            
            foreach (var o in GetComponentsInChildren<FpsLook>())
                o.enabled = false;

            GetComponent<FPSMove>().enabled = false;

        }
    }
}
