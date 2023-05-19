using System;
using System.Collections;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public PlayerManager playerManager;
    
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
            //PlayerManager.Instance.AddEggPoint();
            other.GetComponent<PlayerManager>().AddEggPoint();
            Destroy(gameObject);
        }
    }
}
