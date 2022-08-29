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
}
