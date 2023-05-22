using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Photon.Pun;
public class BombIdleController : MonoBehaviourPunCallbacks
{
    [Header("Bomb Main Component")]
    [SerializeField] private float bombTimer;
    public GameObject mbledos;
    public bool isMbledos;
    
    [Header("Reference")]
    private BebekController bebekController;

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        //bebekController = GameObject.FindGameObjectWithTag("Player").GetComponent<BebekController>();
    }

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        StartCoroutine(BombBebek());
    }

    private IEnumerator BombBebek()
    {
        yield return new WaitForSeconds(bombTimer);
        PhotonNetwork.InstantiateRoomObject(mbledos.name, transform.position, Quaternion.identity);
        isMbledos = true;
        
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isMbledos)
            {
                return;
            }
            
            bebekController = collision.GetComponent<BebekController>();
            bebekController.BebekStuner();
            
            //Destroy(gameObject);
            //PhotonNetwork.Destroy(gameObject);
        }
    }

}
