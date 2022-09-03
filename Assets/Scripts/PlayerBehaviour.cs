using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerControls pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = new PlayerControls();
        pc.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //pc.
    }
}
