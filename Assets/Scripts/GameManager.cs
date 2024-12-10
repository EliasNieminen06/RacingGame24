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
    public AudioSource aS;
    public AudioSource aSfx;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip cdSound;
    public float timeRemainingCountdown;
    public string countdownStr;

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

    private void Start()
    {
        aS.clip = menuMusic;
        aS.loop = true;
        aS.Play();
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
        SceneManager.LoadScene("NewScoreScene");
        aSfx.clip = winSound;
        aSfx.Play();
        aS.clip = menuMusic;
        aS.loop = true;
        aS.Play();
    }

    public void Fail()
    {
        gameOn = false;
        SceneManager.LoadScene("MenuScene");
        aSfx.clip = loseSound;
        aSfx.Play();
        aS.clip = menuMusic;
        aS.loop = true;
        aS.Play();
    }

    public void NewScoreEntered(string name)
    {
        LeaderBoardManager.instance.AddScoreWithMetadata(timer, name);
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
        aS.clip = gameMusic;
        aS.loop = true;
        aS.Play();
        timer = 0;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1);
        aSfx.clip = cdSound;
        aSfx.Play();
        timeRemainingCountdown = countdownTime;
        while (timeRemainingCountdown > 0)
        {
            countdownStr = timeRemainingCountdown.ToString();
            yield return new WaitForSeconds(1);
            timeRemainingCountdown--;
        }
        countdownStr = "GO!";
        gameOn = true;
        yield return new WaitForSeconds(2);
        countdownStr = "";
    }
}
