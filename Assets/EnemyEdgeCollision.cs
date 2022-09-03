using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEdgeCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            // lower player lives
            Destroy(collision.gameObject);
        }
    }
}
