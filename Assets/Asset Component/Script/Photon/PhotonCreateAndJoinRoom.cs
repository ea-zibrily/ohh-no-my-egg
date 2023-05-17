using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PhotonCreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private int maxPlayerInRoom;
    [SerializeField] private string MainGameLevelName;
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        string roomName = joinInput.text;
        
        if (PhotonNetwork.CountOfPlayersInRooms >= maxPlayerInRoom)
        {
            Debug.Log("Room is full. Cannot join.");
            return;
        }

        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(MainGameLevelName);
    }
}
