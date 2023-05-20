using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Photon.Pun;

public class EggSpawner : MonoBehaviourPunCallbacks
{
    [Header("Egg Position Component")] 
    [SerializeField] private float maxEggPositionX;
    [SerializeField] private float minEggPositionX;
    [SerializeField] private float maxEggPositionY;
    [SerializeField] private float minEggPositionY;
    
    [Header("Egg Spawn Component")] 
    [SerializeField] private int eggQuantity;
    [SerializeField] private float eggSpawnTimer;
    public int eggCount { get; set; }
    public GameObject eggPrefabs;
    
    [Header("Reference")]
    private BombSpawner bombSpawner;

    private void Start()
    {
        eggCount = 0;
        // Get the BombSpawner component
        bombSpawner = GameObject.FindGameObjectWithTag("BombSpawner").GetComponent<BombSpawner>();
    
    }
    
    private void Update()
    {
        if (bombSpawner == null || bombSpawner.isBombDestroyed)
        {
            return;
        }
        if(PhotonNetwork.IsMasterClient)
        {
            AyoSpawnEgg();

        }
    }
    
    private void AyoSpawnEgg()
    {
        StartCoroutine(SpawnEggCoroutine());
    }
    
    private IEnumerator SpawnEggCoroutine()
    {
        yield return new WaitForSeconds(eggSpawnTimer);
        
        if (eggCount < eggQuantity)
        {
            var eggRandomPosition = new Vector2(Random.Range(minEggPositionX, maxEggPositionX),
                Random.Range(minEggPositionY, maxEggPositionY));
            
            PhotonNetwork.InstantiateRoomObject(eggPrefabs.name, eggRandomPosition, Quaternion.identity);
            eggCount++;
        }
    }

    
}
