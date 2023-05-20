using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Photon.Pun;

public class BombSpawner : MonoBehaviourPunCallbacks
{
    [Header("Bomb Position Component")] 
    [SerializeField] private float maxBombPositionX;
    [SerializeField] private float minBombPositionX;
    [SerializeField] private float maxBombPositionY;
    [SerializeField] private float minBombPositionY;

    [Header("Bomb Spawn Component")] 
    [SerializeField] private int bombQuantity;
    [SerializeField] private float bombSpawnTimer;
    public bool isBombDestroyed { get; set; }
    public int bombCount { get; set; }
    public GameObject[] bombPrefabs;

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (isBombDestroyed)
        {
            return;
        }

        StartCoroutine(SpawnBombCoroutine());
    }


    private IEnumerator SpawnBombCoroutine()
    {
        yield return new WaitForSeconds(bombSpawnTimer);
        
        if (bombCount < bombQuantity)
        {
            int bombPrefabIndex = Random.Range(0, bombPrefabs.Length);
            var bombRandomPosition = new Vector2(Random.Range(minBombPositionX, maxBombPositionX),
                Random.Range(minBombPositionY, maxBombPositionY));
            
            PhotonNetwork.InstantiateRoomObject(bombPrefabs[bombPrefabIndex].name, bombRandomPosition, Quaternion.identity);
            bombCount++;
        }
        else
        {
            isBombDestroyed = true;
        }
    }

}
