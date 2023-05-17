using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEventHandler : MonoBehaviour
{
    private BombSpawner bombSpawner;

    private void Awake()
    {
        bombSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<BombSpawner>();
    }

    public void BombIsDestroyed()
    {
        bombSpawner.isBombDestroyed = false;
        bombSpawner.bombCount = 0;
    }
    public void DestroyBombAnimation() => Destroy(gameObject);
}
