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

    public void AddPlayerStats(string name, float time)
    {
        PlayerStats newPlayerStats = new PlayerStats();
        newPlayerStats.playerName = name;
        newPlayerStats.playerTime = time;
        playerStatsList.Add(newPlayerStats);
        if(playerStatsList.Count > 1)
        {
            SortList();
        }
    }

    public void SortList()
    {
        int n = playerStatsList.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (playerStatsList[j].playerTime > playerStatsList[j + 1].playerTime)
                {
                    PlayerStats temp = playerStatsList[j];
                    playerStatsList[j] = playerStatsList[j + 1];
                    playerStatsList[j + 1] = temp;
                }
            }
        }
    }
}
