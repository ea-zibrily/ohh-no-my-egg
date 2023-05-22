using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Photon.Pun;

public class BombMoveController : MonoBehaviourPunCallbacks
{
    [Header("Bomb Main Component")] [SerializeField]
    private float bombSpeed;
    [SerializeField] private float bombTimer;
    public GameObject mbledos;
    public bool isMbledos;
    
    [Header("Reference")]
    private BebekController bebekController;
    private Rigidbody2D myRb;

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        //bebekController = GameObject.FindGameObjectWithTag("Player").GetComponent<BebekController>();
        myRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        BombMove();
        StartCoroutine(BombBebek());
    }
    
    private void BombMove()
    {
        float moveX, moveY;
        moveX = Random.Range(0, 2) == 0 ? -1 : 1;
        moveY = Random.Range(0, 2) == 0 ? -1 : 1;

        myRb.velocity = new Vector2(moveX * bombSpeed, moveY * bombSpeed);
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
        if (!PhotonNetwork.IsMasterClient)
            return;

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