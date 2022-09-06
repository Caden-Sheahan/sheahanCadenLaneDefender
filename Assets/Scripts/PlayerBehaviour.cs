/******************************************************************************  
//  File Name:      AudioController.cs     
//  Author:         Caden Sheahan
//  Creation Date:  August 25th, 2022
//      
//  Description:    This script controls player inputs for movement and firing,
                    and the events that occur when inputted.
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    PlayerControls pc;
    Animator anim;

    [Header("Movement")]
    private Vector2 moveInput;
    public int moveSpeed;

    [Header("Attacking")]
    public bool canFire = true;
    public bool fire = true;
    public GameObject bullet;
    public float bOffset;
    [Range(0, 2)]
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        pc = new PlayerControls();
        pc.Enable();

        // input vector2 is saved to "moveInput"
        pc.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        pc.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        pc.Player.Fire.performed += ctx => canFire = true; // extra step to starting the firing coroutine when button is pushed
        pc.Player.Fire.canceled += ctx => canFire = false; // prevents the coroutine from starting on release

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove(moveInput); // moveInput is passed into "PlayerMove" method.
        ReadyToShoot();           
    }

    /// <summary>
    /// 
    /// </summary>
    private void PlayerMove(Vector2 input)
    {
        Vector2 movement; // Actual movement vector2 created
        // set equal to the player input value times speed times time.deltatime
        movement = input * moveSpeed * Time.deltaTime; 
        // set movement vector in transform.translate.
        transform.Translate(movement, Space.World);
    }

    private void ReadyToShoot()
    {
        if (canFire) // once the button is pushed...
        {
            StartCoroutine(Shoot()); // start the coroutine
        }    
    }

    IEnumerator Shoot()
    {
        if (pc.Player.Fire.IsPressed()) // Checks if the button is being held
        {
            if (fire) //if it is, and fire is true...
            {
                // turn it off to prevent bullets from spawning every frame
                fire = false; 
                // spawn a bullet at the PLAYER'S pos + offset so it's by the bell of the horn
                Instantiate(bullet, new Vector3(transform.position.x + bOffset,
                    transform.position.y, 0f), Quaternion.identity);
                // activate fire animation
                anim.SetBool("Fire", true);
                // wait for the set cooldown (can edit in inspector)
                yield return new WaitForSeconds(cooldown);
                fire = true; // after cooldown, set fire to true for next cycle
            }
        }
    }

    /// <summary>
    /// Called at the end of the fire animation as an event to toggle the bool
    /// off and switch back to idle
    /// </summary>
    public void ResetIdle()
    {
        anim.SetBool("Fire", false);
    }
}
