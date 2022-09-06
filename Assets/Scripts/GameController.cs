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
    public GameObject[] enemies = new GameObject[3];
    public Vector3[] spawnPoints = new Vector3[5] {new Vector3(10, 2, 0),
        new Vector3(10, 0.75f, 0), new Vector3(10, -0.5f, 0),
        new Vector3(10, -1.75f, 0), new Vector3(10, -3, 0)};

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
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

    IEnumerator SpawnEnemy(int spawnRate)
    {
        while (true)
        {
            Instantiate(enemies[Random.Range(0, 3)], spawnPoints[Random.Range(0, 4)],
                Quaternion.identity);
            spawnRate = Random.Range(2, 5);
            print(spawnRate);
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
        PlayerPrefs.Save();
    }
    #endregion

    private void HighScore(int score)
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if(score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        hsText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void GameOver()
    {
        if (pLives == 0)
        {
            Time.timeScale = 0;
            goUI.SetActive(true);
        }
    }
}
