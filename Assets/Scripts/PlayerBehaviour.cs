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

        pc.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        pc.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        pc.Player.Fire.performed += ctx => canFire = true;
        pc.Player.Fire.canceled += ctx => canFire = false;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove(moveInput);
        FireBullet();           
    }

    /// <summary>
    /// 
    /// </summary>
    private void PlayerMove(Vector2 input)
    {
        Vector2 movement; //
        //
        movement = input * moveSpeed * Time.deltaTime;
        //
        transform.Translate(movement, Space.World);
    }

    private void FireBullet()
    {
        if (canFire)
        {
            StartCoroutine(FireRate());
        }    
    }

    IEnumerator FireRate()
    {
        if (pc.Player.Fire.IsPressed())
        {
            if (fire)
            {
                fire = false;
                Instantiate(bullet, new Vector3(transform.position.x + bOffset,
                    transform.position.y, 0f), Quaternion.identity);
                anim.SetBool("Fire", true);
                yield return new WaitForSeconds(cooldown);
                fire = true;
            }
        }
    }

    public void ResetIdle()
    {
        anim.SetBool("Fire", false);
    }
}
