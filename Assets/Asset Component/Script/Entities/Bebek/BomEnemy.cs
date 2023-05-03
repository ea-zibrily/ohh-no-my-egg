using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomEnemy : MonoBehaviour
{
    [SerializeField] private float timer;
    public GameObject mbledos;
    public bool isMbledos;

    private void Start()
    {
        StartCoroutine(boomBebek());
    }

    private IEnumerator boomBebek()
    {
        yield return new WaitForSeconds(timer);
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
