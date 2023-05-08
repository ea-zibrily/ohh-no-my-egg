using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BombEnemy : MonoBehaviour
{
    [SerializeField] private float bombTimer;
    public GameObject mbledos;
    public bool isMbledos;

    private void Start()
    {
        StartCoroutine(BombBebek());
    }

    private IEnumerator BombBebek()
    {
        yield return new WaitForSeconds(bombTimer);
        Instantiate(mbledos, transform.position, Quaternion.identity);
        isMbledos = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isMbledos)
            {
                return;
            }
            Debug.Log("Boom");
            Destroy(gameObject);
        }
    }

}
