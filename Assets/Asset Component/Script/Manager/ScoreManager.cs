using Photon.Pun;
using UnityEngine;

public class PointManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private int player1Points;
    private int player2Points;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Initialize the points to default values on the master client
            player1Points = 0;
            player2Points = 0;
        }
    }

    public void AddPointsToPlayer1(int points)
    {
        // Increase the points of Player 1 and update it on all clients
        player1Points += points;
        photonView.RPC("UpdatePlayer1Points", RpcTarget.All, player1Points);
    }

    public void AddPointsToPlayer2(int points)
    {
        // Increase the points of Player 2 and update it on all clients
        player2Points += points;
        photonView.RPC("UpdatePlayer2Points", RpcTarget.All, player2Points);
    }

    [PunRPC]
    private void UpdatePlayer1Points(int newPoints)
    {
        // Update the points of Player 1 on all clients
        player1Points = newPoints;
    }

    [PunRPC]
    private void UpdatePlayer2Points(int newPoints)
    {
        // Update the points of Player 2 on all clients
        player2Points = newPoints;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Synchronize the points over the network
        if (stream.IsWriting)
        {
            stream.SendNext(player1Points);
            stream.SendNext(player2Points);
        }
        else
        {
            player1Points = (int)stream.ReceiveNext();
            player2Points = (int)stream.ReceiveNext();
        }
    }
}
