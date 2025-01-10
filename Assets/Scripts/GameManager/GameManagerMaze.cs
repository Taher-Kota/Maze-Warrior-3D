using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMaze : MonoBehaviour
{
    public TextMeshProUGUI CoinText, HealthText, TimeText;
    public GameObject EndPanel;
    public static GameManagerMaze instance;
    private int Coins = 0;
    private float time = 199f;
    private static int Levels = 1;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        if(Levels == 2)
        {
            time = 299f;
        }
        if(Levels == 3)
        {
            time = 399f;
        }
    }

    private void Update()
    {
        TimeChecker();
    }

    public void CoinIncrement()
    {
        Coins++;
        CoinText.text = "Coins :  " + Coins;
        Sound.instance.CoinSound();
    }

    void TimeChecker()
    {
        time -= Time.deltaTime;
        TimeText.text = "Time :  " + time.ToString("F0");
        if (time <= 0)
        {
            GameOver();
        }
    }

    public void HealthDisplay(int health)
    {
        HealthText.text = "Health :   " + health;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        EndPanel.SetActive(true);
    }

    public void NextLevel()
    {
        Levels++;
        SceneManager.LoadScene(Levels);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(Levels);
    }
}
