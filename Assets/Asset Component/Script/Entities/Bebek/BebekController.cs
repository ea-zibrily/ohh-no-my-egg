using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BebekController : MonoBehaviour
{
    #region Bebek Main Component

    [Serializable]
    public struct BebekStats
    {
        public float bebekSpeed;
        [HideInInspector] public bool bebekNgananLur;
        public Vector2 bebekVelocity;
    }
    
    public BebekStats bebekStats;

    #endregion
    
    #region Reference

    private Rigidbody2D myRb;
    private Animator myAnim;

    #endregion
    
    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        BebekNgambang();
        BebekAnimasi();
        DireksiBebek();
    }

    private void BebekNgambang()
    {
        float x, y;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        bebekStats.bebekVelocity = new Vector2(x, y);
        myRb.velocity = bebekStats.bebekVelocity * bebekStats.bebekSpeed;
    }
    
    private void DireksiBebek()
    {
        if(bebekStats.bebekVelocity.x > 0 && !bebekStats.bebekNgananLur)
        {
            BebekMuter();
        }
        
        if(bebekStats.bebekVelocity.x < 0 && bebekStats.bebekNgananLur)
        {
            BebekMuter();
        }
    }

    private void BebekMuter()
    {
        bebekStats.bebekNgananLur = !bebekStats.bebekNgananLur;
        transform.Rotate(0f, 180f, 0f);
    }

    private void BebekAnimasi()
    {
        if (bebekStats.bebekVelocity != Vector2.zero)
        {
            myAnim.SetFloat("Horizontal", bebekStats.bebekVelocity.x);
            myAnim.SetFloat("Vertical", bebekStats.bebekVelocity.y);
            myAnim.SetBool("isWalk", true);
        }
        else
        {
            myAnim.SetBool("isWalk", false);
        }
    }

}
