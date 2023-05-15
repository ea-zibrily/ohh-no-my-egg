using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [Header("Main Component")] 
    [SerializeField] private float maxBombPositionX;
    [SerializeField] private float minBombPositionX;
    [SerializeField] private float maxBombPositionY;
    [SerializeField] private float minBombPositionY;
    private Transform bombPublicPosition;
    
    [Space]
    [SerializeField] private float bombSpawnTimer;
    public GameObject bombPrefabs;
    
    void Update()
    {
        StartCoroutine(AyoSpawnBom());
    }

    private IEnumerator AyoSpawnBom()
    {
        yield return new WaitForSeconds(bombSpawnTimer);
        
        for (int i = 0; i < 3; i++)
        {
            var bombPosition = bombPublicPosition.position = new Vector2
                (Random.Range(minBombPositionX, maxBombPositionX), Random.Range(minBombPositionY, maxBombPositionY));
            
            Instantiate(bombPrefabs, bombPosition, Quaternion.identity);
        }
    }
}
