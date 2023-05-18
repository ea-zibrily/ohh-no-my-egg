using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BombIdleController : MonoBehaviour
{
    [Header("Bomb Main Component")]
    [SerializeField] private float bombTimer;
    public GameObject mbledos;
    public bool isMbledos;
    
    [Header("Reference")]
    private BebekController bebekController;

    private void Awake()
    {
        bebekController = GameObject.FindGameObjectWithTag("Player").GetComponent<BebekController>();
    }

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
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isMbledos)
            {
                return;
            }
            
            bebekController.BebekStuner();
            Destroy(gameObject);
        }
    }

}
