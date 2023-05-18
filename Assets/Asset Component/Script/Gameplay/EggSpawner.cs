using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EggSpawner : MonoBehaviour
{
    [Header("Bomb Position Component")] 
    [SerializeField] private float maxEggPositionX;
    [SerializeField] private float minEggPositionX;
    [SerializeField] private float maxEggPositionY;
    [SerializeField] private float minEggPositionY;
    
    [Header("Bomb Spawn Component")] 
    [SerializeField] private int eggQuantity;
    [SerializeField] private float eggSpawnTimer;
    public int eggCount { get; set; }
    public GameObject eggPrefabs;
    
    [Header("Reference")]
    private BombSpawner bombSpawner;

    private void Awake()
    {
        bombSpawner = GameObject.FindGameObjectWithTag("BombSpawner").GetComponent<BombSpawner>();
    }

    private void Start()
    {
        eggCount = 0;
    }

    private void Update()
    {
        if (bombSpawner.isBombDestroyed)
        {
            return;
        }
        
        StartCoroutine(AyoSpawnBom());
    }

    private IEnumerator AyoSpawnBom()
    {
        yield return new WaitForSeconds(eggSpawnTimer);
        
        if (eggCount < eggQuantity)
        {
            var eggRandomPosition = new Vector2(Random.Range(minEggPositionX, maxEggPositionX),
                Random.Range(minEggPositionY, maxEggPositionY));
            
            Instantiate(eggPrefabs, eggRandomPosition, Quaternion.identity);
            eggCount++;
        }
    }
}
