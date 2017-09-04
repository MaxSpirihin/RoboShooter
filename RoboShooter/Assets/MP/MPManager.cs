using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPManager : Photon.PunBehaviour {

    public static MPManager instance;
    GameObject localPlayer;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

        PhotonNetwork.automaticallySyncScene = true;

    }


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("RoboShooter_Alpha");
    }


    public void JoinGame()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("Default Room", ro, null);
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel("MP/MPArena");
        }
    }

    void OnLevelWasLoaded(int levelNumber)
    {
        if (!PhotonNetwork.inRoom) return;
        Debug.Log(PhotonNetwork.isMasterClient);

        localPlayer = PhotonNetwork.Instantiate(
            "PlayerMP",
            new Vector3(0, 100f, 0),
            Quaternion.identity, 0);
    }
}
