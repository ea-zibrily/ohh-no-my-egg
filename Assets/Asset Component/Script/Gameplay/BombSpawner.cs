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
    [SerializeField] private float bombSpawnTimer;
    [SerializeField] private bool isBombDestroyed;
    private int bombCount;
    public GameObject bombPrefabs;
    
    [Header("Reference")]
    private BombEventHandler bombEventHandler;


    private void OnEnable()
    {
        bombEventHandler.OnBombDestroy += BombDestroyed;
    }
    
    private void OnDisable()
    {
        bombEventHandler.OnBombDestroy -= BombDestroyed;
    }

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
        
        if (bombCount < 3)
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
