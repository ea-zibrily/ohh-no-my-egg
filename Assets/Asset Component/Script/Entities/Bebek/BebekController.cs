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
        public bool bebekNgananLur;
        public bool bebekSatu;
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
        DireksiBebek();
    }

    private void BebekNgambang()
    {
        float x, y;

        if(bebekStats.bebekSatu)
        {
            x = Input.GetAxisRaw("Horizontal1");
            y = Input.GetAxisRaw("Vertical1");
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal2");
            y = Input.GetAxisRaw("Vertical2");
        }
        
        myRb.velocity = new Vector2(x * bebekStats.bebekSpeed, y * bebekStats.bebekSpeed);
    }
    
    private void DireksiBebek()
    {
        var bebekVelocity = myRb.velocity;
        
        if(bebekVelocity.x > 0 && !bebekStats.bebekNgananLur)
        {
            BebekMuter();
        }
        
        if(bebekVelocity.x < 0 && bebekStats.bebekNgananLur)
        {
            BebekMuter();
        }
    }

    private void BebekMuter()
    {
        bebekStats.bebekNgananLur = !bebekStats.bebekNgananLur;
        transform.Rotate(0f, 180f, 0f);
    }

}
