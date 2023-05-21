using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;

public class EggController : MonoBehaviourPunCallbacks
{
    //public PlayerManager playerManager;
    
    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        StartCoroutine(DestroyEgg());
    }
    
    private IEnumerator DestroyEgg()
    {
        yield return new WaitForSeconds(7f);
        PhotonNetwork.Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView otherPhotonView = other.GetComponent<PhotonView>();
            if(otherPhotonView.Owner.IsMasterClient)
            {
                PointManager.Instance.AddPointsToPlayer1(1);
            }
            else
            {
                PointManager.Instance.AddPointsToPlayer2(1);
            }
            //PlayerManager.Instance.AddEggPoint();
            //other.GetComponent<PlayerManager>().AddEggPoint();
            Destroy(gameObject);
        }
    }
}
