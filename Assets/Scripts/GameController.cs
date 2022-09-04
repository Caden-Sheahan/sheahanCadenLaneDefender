using System;
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

        pc.Player.Quit.performed += ctx => Quit();
        pc.Player.Reset.performed += ctx => Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Inputs
    private void Reset()
    {
        Debug.Log("Reset Game!");
        SceneManager.LoadScene(0);
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    private void OnDisable()
    {
        pc.Disable();
    }
    #endregion
}
