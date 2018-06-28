using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public WaveSpawn[] waveSpawns;
    public Text timeToNewWaveText;
    public float timeToNewWave;
    public Text currentMoneyText;
    public int currentMoney;
    public bool isWaveStarted;
    public GameObject endGameScreen;
    public int totalWaves;

    int lastWaveEnemyCount;
    int lastWaveEnemyHP;
    int currentWaveSpawnsWaveEndedCallbacks;
    float currentTimeToNewWave;
    int currentWave;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
        currentTimeToNewWave = timeToNewWave;
        currentMoney = 10;
    }
	
	// Update is called once per frame
	void Update () {
        currentTimeToNewWave = Mathf.Clamp(currentTimeToNewWave - Time.deltaTime, 0, currentTimeToNewWave);
        currentMoneyText.text = "Money : " + currentMoney.ToString();
        timeToNewWaveText.text = "Time to next wave : " + Mathf.Ceil(currentTimeToNewWave).ToString();
        if (!isWaveStarted && currentTimeToNewWave <= 0)
        {
            StartNewWave();
        }
	}

    void StartNewWave()
    {
        isWaveStarted = true;
        lastWaveEnemyHP += 10;
        lastWaveEnemyCount += 3;
        foreach (WaveSpawn item in waveSpawns)
        {
            item.StartNextWave(lastWaveEnemyHP, lastWaveEnemyCount, 1.5f);
        }
    }

    public void EndWave()
    {
        currentWaveSpawnsWaveEndedCallbacks++;
        if (currentWaveSpawnsWaveEndedCallbacks >= waveSpawns.Length)
        {
            currentWaveSpawnsWaveEndedCallbacks = 0;
            WaveFinished();
        }
    }

    void WaveFinished()
    {
        currentWave++;
        if (currentWave >= totalWaves)
        {
            WinGame();
        }
        currentTimeToNewWave = timeToNewWave;
        isWaveStarted = false;
    }

    void WinGame()
    {
        Time.timeScale = 0.1f;
        endGameScreen.SetActive(true);
        endGameScreen.GetComponentInChildren<Text>().text = "You win!";
        Invoke("BackToMainMenu", 0.2f);
    }

    public void LoseGame()
    {
        Time.timeScale = 0.1f;
        endGameScreen.SetActive(true);
        endGameScreen.GetComponentInChildren<Text>().text = "You lose!";
        Invoke("BackToMainMenu", 0.2f);
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
