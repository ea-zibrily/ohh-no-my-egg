using System;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable//MonoSingleton<PlayerManager>
{
    [Header("Egg Point Component")]
    private int currentEggPoint;
    public TextMeshProUGUI eggPointText;

    private void Start()
    {
        currentEggPoint = 0;

        // Assign UI text based on player's photonView ID
        if (photonView.IsMine)
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
        //photonView.RPC("AddEggPointRPC", RpcTarget.All);
        currentEggPoint++;
        eggPointText.text = currentEggPoint.ToString();
    }

    [PunRPC]
    private void AddEggPointRPC()
    {
        currentEggPoint++;
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
    
}

