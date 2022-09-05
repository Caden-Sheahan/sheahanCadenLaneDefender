using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Collider2D col;

    public float bSpeed;
    private Vector2 bMove;
    private bool bActive = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
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
        if (collision.gameObject.CompareTag("enemy"))
        {
            bActive = false;
            col.enabled = false;
            anim.SetTrigger("BulletHit");
        }
    }

    public void BulletDestroy()
    {
        Destroy(gameObject);
    }
}
