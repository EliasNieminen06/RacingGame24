using UnityEngine;
using System.Collections.Generic;

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

    private void Awake()
    {
        instance = this;
    }

    public void AddPlayerStats(string name, float time)
    {
        PlayerStats newPlayerStats = new PlayerStats();
        newPlayerStats.playerName = name;
        newPlayerStats.playerTime = time;
        playerStatsList.Add(newPlayerStats);
    }
}
