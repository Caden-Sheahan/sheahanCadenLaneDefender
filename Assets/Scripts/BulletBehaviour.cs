using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bSpeed;
    private Vector2 bMove;

    // Update is called once per frame
    void Update()
    {
        transform.position = bMove;
        bMove = bSpeed * Vector2.right * Time.deltaTime;
        bMove = transform.position;
    }
}
