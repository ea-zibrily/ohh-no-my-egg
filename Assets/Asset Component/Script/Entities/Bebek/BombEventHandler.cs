using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEventHandler : MonoBehaviour
{
    public event Action OnBombDestroy;
    
    public void BombIsDestroyed() => OnBombDestroy?.Invoke();
    public void DestroyBombAnimation() => Destroy(gameObject);
}
