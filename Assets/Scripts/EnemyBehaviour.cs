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
    [Header("Spawning")]
    public GameObject[] spawnPoints = new GameObject[5]; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            // reference animator and start death anim.
        }
        if (collision.gameObject.CompareTag("player"))
        {
            // player loses a life
            EnemyDeath();

        }
        if (collision.gameObject.CompareTag("border"))
        {
            // player loses a life
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
