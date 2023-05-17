using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    }
    
    public BebekStats bebekStats;

    #endregion
    
    #region Reference

    private Rigidbody2D myRb;
    private Animator myAnim;
    private PhotonView myView;

    #endregion
    
    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myView = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        //make so player can only move their own character
        if(myView.IsMine)
        {
            BebekNgambang();
            DireksiBebek();
        }
    }

    private void BebekNgambang()
    {
        float x, y;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

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
