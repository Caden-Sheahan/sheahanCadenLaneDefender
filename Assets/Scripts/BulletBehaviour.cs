/******************************************************************************  
//  File Name:      BulletBehaviour.cs     
//  Author:         Caden Sheahan
//  Creation Date:  September 4th, 2022
//      
//  Description:    This script controls the treble clef bullets operate. Their
//                  movement and the events where they are destroyed.
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Collider2D col;

    public float bSpeed;
    private Vector2 bMove; // movement vector for bullet
    private bool bActive = true; // checks if bullets are moving / are able to

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (transform.position.x > 11) // if bullets go off screen
        {
            BulletDestroy(); // *poof*
        }
    }
    // Update is called once per frame
    void FixedUpdate() // Same movement system as enemies
    {
        if (bActive)
        {
            bMove = Vector2.right * bSpeed;
        }
        else
        {
            bMove = Vector2.zero;
        }
        rb.velocity = bMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy")) // If a bullet hits an enemy...
        {
            bActive = false; // stop moving
            col.enabled = false; // disable collider
            anim.SetTrigger("BulletHit"); // Start bullet hit animation
        }
    }

    /// <summary>
    /// Referenced at the end of the hit animation to get rid of the bullets
    /// </summary>
    public void BulletDestroy()
    {
        Destroy(gameObject);
    }
}
