/******************************************************************************  
//  File Name:      GameController.cs        
//  Author:         Caden Sheahan
//  Creation Date:  August 25th, 2022
//      
//  Description:    This script controls the UI of the game, including the high
                    score, current score, and lives. It also controls the 
                    spawning of the enemies, the Game Over sequence, and the
                    Debug inputs of restarting or quitting the game.
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerControls pc;

    [Header("HUD")]
    public int pLives = 3;
    public TMPro.TMP_Text lText;

    public int pScore = 0;
    public TMPro.TMP_Text sText;
    
    public TMPro.TMP_Text hsText;

    public GameObject goUI;

    [Header("Enemies")]
    public int spawnRate;
    public GameObject[] enemies = new GameObject[3]; // differentiates between enemies
    public Vector3[] spawnPoints = new Vector3[5] {new Vector3(10, 2, 0), // spawn coords for each lane
        new Vector3(10, 0.75f, 0), new Vector3(10, -0.5f, 0),
        new Vector3(10, -1.75f, 0), new Vector3(10, -3, 0)};

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; // Reset time scale when scene is reset with "R"
        StartCoroutine(SpawnEnemy(spawnRate));

        pc = new PlayerControls();
        pc.Enable();

        pc.Player.Quit.performed += ctx => Quit();
        pc.Player.Reset.performed += ctx => Reset();
    }

    // Update is called once per frame
    void Update()
    {
        lText.text = "Lives: " + pLives.ToString();
        sText.text = "Score: " + pScore.ToString();
        HighScore(pScore);
        GameOver();
    }

    /// <summary>
    /// Instantiates a random one of 3 types of enemies at in a random lane. 
    /// Also sets a new spawn rate between 2-3 for each new enemy spawned.
    /// </summary>
    /// <param name="spawnRate"></param>
    /// <returns></returns>
    IEnumerator SpawnEnemy(int spawnRate)
    {
        while (true)
        {
            Instantiate(enemies[Random.Range(0, 3)], spawnPoints[Random.Range(0, 5)],
                Quaternion.identity);
            spawnRate = Random.Range(2, 4); // only between 2-3. Why does the upper bound never get selected? ask
            yield return new WaitForSeconds(spawnRate);
        }
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
        PlayerPrefs.Save(); // Save high score!
    }
    #endregion

    private void HighScore(int score)
    {
        if (PlayerPrefs.HasKey("HighScore")) // if high score exists...
        {
            if(score > PlayerPrefs.GetInt("HighScore")) // AND the score is higher than it...
            {
                PlayerPrefs.SetInt("HighScore", score); // update it
            }
        }
        else // if it doesn't exist...
        {
            PlayerPrefs.SetInt("HighScore", score); // make it!
        }
        hsText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString(); // display on UI
    }

    private void GameOver()
    {
        if (pLives == 0)
        {
            Time.timeScale = 0; // pause game 
            goUI.SetActive(true); // show game over UI with controls to quit or restart
        }
    }
}
