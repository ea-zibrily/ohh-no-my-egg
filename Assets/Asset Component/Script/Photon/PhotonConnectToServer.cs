using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Realtime;

public class PhotonConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //photonView.RPC("NotifyOtherPlayers", RpcTarget.All);
        //NotifyPlayers();
        SceneManager.LoadScene("Lobby");
    }

    //[PunRPC]
    // private void NotifyPlayers()
    // {
    //     Debug.Log("A player has joined the server.");
    //     // Show the notification to other players or perform any other actions
    //     Debug.Log("Players in server (including rooms): " + PhotonNetwork.CountOfPlayers);
    // }
}
