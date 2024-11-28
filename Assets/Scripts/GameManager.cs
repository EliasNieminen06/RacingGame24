using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalLaps = 3;
    public float timer;
    public string formatedTime;
    public bool gameOn;
    public bool paused = false;
    public int countdownTime;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (gameOn)
        {
            UpdateGame();
        }
    }

    void UpdateGame()
    {
        timer += Time.deltaTime;
        if (FinishLine.instance.currentLap > totalLaps && gameOn)
        {
            Finish();
        }

        float time = GameManager.instance.timer;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float millidseconds = Mathf.FloorToInt((time * 1000) % 1000);
        formatedTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, millidseconds);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void Finish()
    {
        gameOn = false;
        LeaderBoardManager.instance.AddScoreWithMetadata(timer, Random.Range(100, 999).ToString());
        SceneManager.LoadScene("MenuScene");
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            Application.targetFrameRate = 30;
        }
        else
        {
            Time.timeScale = 1;
            Application.targetFrameRate = 144;
        }
    }

    public void StartGame()
    {
        timer = 0;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float timeRemaining = countdownTime;

        while (timeRemaining > 0)
        {
            print(timeRemaining + " seconds left");
            yield return new WaitForSeconds(1);
            timeRemaining--;
        }
        print("GO!");
        gameOn = true;
    }
}
