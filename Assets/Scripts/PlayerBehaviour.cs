using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerControls pc;

    private Vector2 moveInput;

    public int moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        pc = new PlayerControls();
        pc.Enable();

        pc.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        pc.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove(moveInput);
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
}
