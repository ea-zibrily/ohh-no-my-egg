using System;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks//MonoSingleton<PlayerManager>
{
    /*
    [Header("Egg Point Component")]
    private int currentEggPoint;
    public TextMeshProUGUI eggPointText;

    private void Start()
    {
        currentEggPoint = 0;

        // Assign UI text based on player's photonView ID
        if (PhotonNetwork.IsMasterClient)
        {
            eggPointText = GameObject.FindGameObjectWithTag("PlayerOnePoint").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            eggPointText = GameObject.FindGameObjectWithTag("PlayerTwoPoint").GetComponent<TextMeshProUGUI>();
        }
    }

    public void AddEggPoint()
    {
        currentEggPoint++;

        photonView.RPC("DisplayEggPoint", RpcTarget.All);
    }

    [PunRPC]
    public void DisplayEggPoint()
    {
        eggPointText.text = currentEggPoint.ToString();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentEggPoint);
        }
        else if (stream.IsReading)
        {
            currentEggPoint = (int)stream.ReceiveNext();
            eggPointText.text = currentEggPoint.ToString();
        }
    }
    */
    
}

