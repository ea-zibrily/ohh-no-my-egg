using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombEventHandler : MonoBehaviourPunCallbacks
{
    private BombSpawner bombSpawner;
    private EggSpawner eggSpawner;

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        bombSpawner = GameObject.FindGameObjectWithTag("BombSpawner").GetComponent<BombSpawner>();
        eggSpawner = GameObject.FindGameObjectWithTag("EggSpawner").GetComponent<EggSpawner>();
    }

    public void BombIsDestroyed()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        bombSpawner.isBombDestroyed = false;
        bombSpawner.bombCount = 0;
        eggSpawner.eggCount = 0;
    }
    public void DestroyBombAnimation() => Destroy(gameObject);
}
