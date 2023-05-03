using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BebekController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool nganan;

    private Rigidbody2D myRb;

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bebekNgambang();
    }

    private void bebekNgambang()
    {
        float x, y;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        myRb.velocity = new Vector2(x * speed, y * speed);

        if(x > 0 && !nganan)
        {
            bebekMbalek();
        }
        if(x < 0 && nganan)
        {
            bebekMbalek();
        }
    }

    private void bebekMbalek()
    {
        nganan = !nganan;
        transform.Rotate(0f, 180f, 0f);
    }

}
