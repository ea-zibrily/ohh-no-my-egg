using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEventHandler : MonoBehaviour
{
    private BombSpawner bombSpawner;
    private EggSpawner eggSpawner;

    private void Awake()
    {
        bombSpawner = GameObject.FindGameObjectWithTag("BombSpawner").GetComponent<BombSpawner>();
        eggSpawner = GameObject.FindGameObjectWithTag("EggSpawner").GetComponent<EggSpawner>();
    }

    public void BombIsDestroyed()
    {
        bombSpawner.isBombDestroyed = false;
        bombSpawner.bombCount = 0;
        eggSpawner.eggCount = 0;
    }
    public void DestroyBombAnimation() => Destroy(gameObject);
}
