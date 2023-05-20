using Photon.Pun;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private int player1Points;
    private int player2Points;

    [SerializeField] private TextMeshProUGUI player1PointsDisplay;
    [SerializeField] private TextMeshProUGUI player2PointsDisplay;

    public static PointManager Instance {get; private set;}
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        photonView.RPC("UpdatePlayer1Points", RpcTarget.Others, player1Points);
    }

    public void AddPointsToPlayer2(int points)
    {
        // Increase the points of Player 2 and update it on all clients
        player2Points += points;
        photonView.RPC("UpdatePlayer2Points", RpcTarget.Others, player2Points);
    }

    [PunRPC]
    private void UpdatePlayer1Points(int newPoints)
    {
        // Update the points of Player 1 on all clients
        player1Points = newPoints;
        player1PointsDisplay.text = player1Points.ToString();

    }

    [PunRPC]
    private void UpdatePlayer2Points(int newPoints)
    {
        // Update the points of Player 2 on all clients
        player2Points = newPoints;
        player2PointsDisplay.text = player2Points.ToString();
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
