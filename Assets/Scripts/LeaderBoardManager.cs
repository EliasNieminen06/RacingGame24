using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;

[System.Serializable]
public class PlayerStats
{
    public string playerName;
    public float playerTime;
}

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager instance;

    public List<PlayerStats> playerStatsList = new List<PlayerStats>();
    public string leaderboardID = "TimeLeaderboard";

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

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, 0);
    }

    public void AddPlayerStats(string name, float time)
    {
        PlayerStats newPlayerStats = new PlayerStats();
        newPlayerStats.playerName = name;
        newPlayerStats.playerTime = time;
        playerStatsList.Add(newPlayerStats);
    }

    public async void LoadLeaderboard()
    {
        LeaderboardScoresPage leaderboardScoresPage = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardID);
    }
}
