using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerControls pc;

    [Header("HUD")]
    public static int pLives = 3;
    public TMPro.TMP_Text livesText;

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
        livesText.text = "Lives: " + pLives.ToString();
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
