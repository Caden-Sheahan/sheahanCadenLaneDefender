/******************************************************************************  
//  File Name:      EnemyBehaviour     
//  Author:         Caden Sheahan
//  Creation Date:  August 25th, 2022
//      
//  Description:    This script controls the enemies the player defeats. How
//                  they move, how they spawn, and how they are animated when 
//                  certain events occur.
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Animator anim;

    public GameObject[] spawnPoints = new GameObject[5];
    public int eType;

    public float eHP;
    public float eSpeed;
    private Vector2 eMove;
    private bool eActive = true;
    [Range(0, 2)]
    public int eStun;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (eActive)
        {
            eMove = Vector2.left * eSpeed;
        }
        else
        {
            eMove = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            // reference animator and start death anim.
            // award points
            eHP--;
            print(eHP);
            if (eHP == 0)
            {
                switch (eType)
                {
                    case 0:
                        anim.SetTrigger("8thDied");
                        break;
                    case 1:
                        anim.SetTrigger("QuarterDied");
                        break;
                    case 2:
                        anim.SetTrigger("HalfDied");
                        break;
                    default:
                        break;
                }
            }
            else if (eHP > 0)
            {
                eActive = false;
                switch (eType)
                {
                    case 0:
                        anim.SetBool("8thHurt", true);
                        break;
                    case 1:
                        anim.SetBool("QuarterHurt", true);
                        break;
                    case 2:
                        anim.SetBool("HalfHurt", true);
                        break;
                    default:
                        break;
                }
            }
        }
        if (collision.gameObject.CompareTag("player"))
        {
            GameController.pLives--;
            EnemyDeath();
        }
        if (collision.gameObject.CompareTag("border"))
        {
            // player loses a life
            EnemyDeath();
        }
    }

    IEnumerator EnemyHurt()
    {

        yield return new WaitForSeconds(eStun);
    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
