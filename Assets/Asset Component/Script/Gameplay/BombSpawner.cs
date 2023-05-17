using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BombSpawner : MonoBehaviour
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
    public GameObject bombPrefabs;

    private void Update()
    {
        if (isBombDestroyed)
        {
            return;
        }
        
        StartCoroutine(AyoSpawnBom());
    }

    private IEnumerator AyoSpawnBom()
    {
        yield return new WaitForSeconds(bombSpawnTimer);
        
        if (bombCount < bombQuantity)
        {
            var bombRandomPosition = new Vector2(Random.Range(minBombPositionX, maxBombPositionX),
                Random.Range(minBombPositionY, maxBombPositionY));
            
            Instantiate(bombPrefabs, bombRandomPosition, Quaternion.identity);
            bombCount++;
        }
        else
        {
            isBombDestroyed = true;
        }
    }

    private void BombDestroyed()
    {
        isBombDestroyed = false;
        bombCount = 0;
    }
    
}
