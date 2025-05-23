using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public List<TextMeshProUGUI> leaderSTR = new List<TextMeshProUGUI>();

    public Canvas mainMenuCanvas;
    public Canvas optionsMenuCanvas;
    public Canvas controlsMenuCanvas;
    public Button mms;
    public Button oms;
    public Button cms;

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

    void Start()
    {
        UpdateLeaderboard();
        mms.Select();
    }

    public void ToggleOptions()
    {
        mainMenuCanvas.enabled = !mainMenuCanvas.enabled;
        optionsMenuCanvas.enabled = !optionsMenuCanvas.enabled;
        if (!mainMenuCanvas.enabled) oms.Select();
        else mms.Select();
    }

    public void ToggleControls()
    {
        mainMenuCanvas.enabled = !mainMenuCanvas.enabled;
        controlsMenuCanvas.enabled = !controlsMenuCanvas.enabled;
        if (!mainMenuCanvas.enabled) cms.Select();
        else mms.Select();
    }

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
        else
        {
            leaderSTR[0].text = "No Entries Found.";
        }
    }

    public void GoToGame()
    {
        GameManager.instance.GoToGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
