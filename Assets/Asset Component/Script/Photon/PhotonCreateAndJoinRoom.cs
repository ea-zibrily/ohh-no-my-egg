
using UnityEngine;
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

    [SerializeField] private TextMeshProUGUI PlayerCountText;

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

    private void Update()
    {   
        //photonView.RPC("UpdatePlayerCountRPC", RpcTarget.All);
        PlayerCountText.text = string.Format("Player Count : {0}/{1}", PhotonNetwork.CountOfPlayers, 20);

    }

    [PunRPC]
    private void UpdatePlayerCountRPC()
    {
        PlayerCountText.text = string.Format("Player Count : {0}/{0}", PhotonNetwork.CountOfPlayers, 20);
    }
}
