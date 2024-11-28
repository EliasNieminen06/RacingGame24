using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using System.Threading.Tasks;

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
        GetScoresWithMetadata();
    }

    public void AddPlayerStats(string name, float time)
    {
        PlayerStats newPlayerStats = new PlayerStats();
        newPlayerStats.playerName = name;
        newPlayerStats.playerTime = time;
        playerStatsList.Add(newPlayerStats);
    }

    public async void AddScoreWithMetadata(float time, string name)
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignOut(true);
        }
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, time, new AddPlayerScoreOptions { Metadata = new Dictionary<string, string>() { { "playerName", name } } });
        GetScoresWithMetadata();
    }

    public async void GetScoresWithMetadata()
    {
        playerStatsList.Clear();
        var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID, new GetScoresOptions { IncludeMetadata = true });
        for (int i = 0; i < scoreResponse.Results.Count; i++)
        {
            PlayerStats name = JsonUtility.FromJson<PlayerStats>(scoreResponse.Results[i].Metadata);
            AddPlayerStats(name.playerName, (float)scoreResponse.Results[i].Score);
            MenuManager.instance.UpdateLeaderboard();
        }
    }
}
