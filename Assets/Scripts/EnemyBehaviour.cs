/******************************************************************************  
//  File Name:      EnemyBehaviour.cs     
//  Author:         Caden Sheahan
//  Creation Date:  August 25th, 2022
//      
//  Description:    This script controls the enemies the player defeats. How
//                  they move, how they spawn, and how they are animated when 
//                  certain events occur. Switch statements are used to 
//                  differentiate between the different types of enemies.
//                  All interactions are coded in this script.
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Collider2D col;
    GameController gc;
    AudioController sound;

    public int eType; // used in switches. Set in inspector
    public float eHP;
    public float eSpeed;
    private Vector2 eMove; // movement controls for enemies
    private bool eActive = true; // bool to check if they're moving/able to move

    // Start is called before the first frame update
    void Start()
    { 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        gc = FindObjectOfType<GameController>();
        sound = FindObjectOfType<AudioController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (eActive) // if they are moving / are able to move...
        {
            eMove = Vector2.left * eSpeed; // continue moving left
        }
        else // if they just got hit or died...
        {
            eMove = Vector2.zero; // stop moving
        }
        rb.velocity = eMove; // set vectors equal to velocity of rigidbody
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet")) // if smacked by bullet
        {
            eHP--; // lose a Hit Point
            if (eHP == 0) // if HP is zero...
            {
                eActive = false; // stop moving
                col.enabled = false; // disable collider (animation goes crazy and collider is on the circle bit)
                sound.Play("EnemyDeath"); // play death sound
                switch (eType) // find what type of enemy it is.
                {
                    case 0: // 8th note
                        gc.pScore += 100;
                        anim.SetTrigger("8thDied");
                        break;
                    case 1: // Quarter Note
                        gc.pScore += 300;
                        anim.SetTrigger("QuarterDied");
                        break;
                    case 2: // Half Note
                        gc.pScore += 500;
                        anim.SetTrigger("HalfDied");
                        break;
                    default:
                        break;
                }
            }
            else if (eHP > 0) // if HP is not zero...
            {
                eActive = false; // stop moving still 
                sound.Play("EnemyHit"); // Play hit sound
                switch (eType) // find enemy type
                {
                    case 1: // Quarter
                        anim.SetBool("QuarterHurt", true);
                        break;
                    case 2: // Half
                        anim.SetBool("HalfHurt", true);
                        break;
                    default:
                        break;
                }
            }
        }
        if (collision.gameObject.CompareTag("player")) // If you touch a player...
        {
            gc.pLives--; // player loses a life 
            sound.Play("LifeLost"); // play life lost sound
            EnemyDeath(); // go kablooey (just destroy object, no anim, doesn't really fit)
        }
        if (collision.gameObject.CompareTag("border"))
        {
            gc.pLives--; // player loses a life
            sound.Play("LifeLost"); // play life lost sound
            EnemyDeath(); // exploooooode (just destroy object, no anim, doesn't really fit)
        }
    }

    /// <summary>
    /// Called at the end of the hit animation on enemies with >1 HP. 
    /// Reenebles them to move
    /// </summary>
    public void MoveEnemy()
    {
        eActive = true; 
        switch (eType)
        {
            case 1:
                anim.SetBool("QuarterHurt", false);
                break;
            case 2:
                anim.SetBool("HalfHurt", false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// called on player and end of screen interactions, but also at the end of
    /// the death animation.
    /// </summary>
    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
