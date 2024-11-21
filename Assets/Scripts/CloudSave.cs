using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.CloudSave.Models.Data.Player;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;
using System.Collections.Generic;

public class CloudSave : MonoBehaviour
{
    public static CloudSave instance;

    private async void Awake()
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
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void SaveLeaderBoardData(List<PlayerStats> leaderData)
    {
        var data = new Dictionary<string, object> {{"LeaderData", leaderData}};
        await CloudSaveService.Instance.Data.Player.SaveAsync(data, new SaveOptions(new PublicWriteAccessClassOptions()));
    }

    //public async List<PlayerStats> LoadLeaderBoardData()
    //{
    //    var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "LeaderData" }, new LoadOptions(new PublicReadAccessClassOptions()));
    //    if (playerData.TryGetValue("LeaderData", out var keyName))
    //    {
    //        return playerData;
    //    }
    //}
}
