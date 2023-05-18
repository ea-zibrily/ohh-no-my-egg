using System;
using System.Collections;
using UnityEngine;

public class EggController : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(DestroyEgg());
    }
    
    private IEnumerator DestroyEgg()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.AddEggPoint();
            Destroy(gameObject);
        }
    }
}