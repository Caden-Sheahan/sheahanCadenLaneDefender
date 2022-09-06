/******************************************************************************  
//  File Name:      EnemyBehaviour     
//  Author:         Caden Sheahan
//  Creation Date:  August 25th, 2022
//      
//  Description:    This script controls the enemies the player defeats. How
//                  they move, how they spawn, and how they are animated when 
//                  certain events occur. Switch statements are used to 
//                  differentiate between the different types of enemies.
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

    //public GameObject enemy;
    //public Vector3[] spawnPoints = new Vector3[5] {new Vector3(10, 2, 0), 
    //    new Vector3(10, 0.75f, 0), new Vector3(10, -0.5f, 0), 
    //    new Vector3(10, -1.75f, 0), new Vector3(10, -3, 0)};
    //public int sRate;

    public int eType;
    public float eHP;
    public float eSpeed;
    private Vector2 eMove;
    private bool eActive = true;

    // Start is called before the first frame update
    void Start()
    { 

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        gc = FindObjectOfType<GameController>();
    }

    //IEnumerator SpawnEnemy()
    //{
    //    while (true)
    //    {
    //        Instantiate(enemy, spawnPoints[Random.Range(0, 4)], 
    //            Quaternion.identity);
    //        yield return new WaitForSeconds(sRate);
    //    }
    //}

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
        rb.velocity = eMove;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            eHP--;
            if (eHP == 0)
            {
                eActive = false;
                col.enabled = false;
                switch (eType)
                {
                    case 0:
                        gc.pScore += 100;
                        anim.SetTrigger("8thDied");
                        break;
                    case 1:
                        gc.pScore += 300;
                        anim.SetTrigger("QuarterDied");
                        break;
                    case 2:
                        gc.pScore += 500;
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
            gc.pLives--;
            EnemyDeath();
        }
        if (collision.gameObject.CompareTag("border"))
        {
            gc.pLives--;
            EnemyDeath();
        }
    }

    public void MoveEnemy()
    {
        eActive = true;
        switch (eType)
        {
            case 0:
                anim.SetBool("8thHurt", false);
                break;
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

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
