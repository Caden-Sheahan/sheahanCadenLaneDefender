using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
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
        GameDebugging();
    }
    private void GameDebugging()
    {

    }
}
