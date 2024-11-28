using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public List<TextMeshProUGUI> leaderSTR = new List<TextMeshProUGUI>();

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLeaderboard();
    }

    // Update is called once per frame
    public void UpdateLeaderboard()
    {
        if (LeaderBoardManager.instance.playerStatsList.Count != 0)
        {
            for (int i = 0; i < leaderSTR.Count; i++)
            {
                if (i < LeaderBoardManager.instance.playerStatsList.Count)
                {
                    float time = LeaderBoardManager.instance.playerStatsList[i].playerTime;
                    float minutes = Mathf.FloorToInt(time / 60);
                    float seconds = Mathf.FloorToInt(time % 60);
                    float millidseconds = Mathf.FloorToInt((time * 1000) % 1000);
                    string formatedTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, millidseconds);
                    leaderSTR[i].text = formatedTime + " " + LeaderBoardManager.instance.playerStatsList[i].playerName;
                }
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        GameManager.instance.StartGame();
    }
}
