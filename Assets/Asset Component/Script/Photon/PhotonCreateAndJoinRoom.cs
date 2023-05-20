using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonCreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private int roomSize;
    //[SerializeField] private int maxPlayerInRoom;
    [SerializeField] private string WaitingRoom;
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        PhotonNetwork.CreateRoom(createInput.text, roomOps);
    }

    public void JoinRoom()
    {
        string roomName = joinInput.text;
        
        // if (PhotonNetwork.CountOfPlayersInRooms >= maxPlayerInRoom)
        // {
        //     Debug.Log("Room is full. Cannot join.");
        //     return;
        // }

        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(WaitingRoom);
    }
}
