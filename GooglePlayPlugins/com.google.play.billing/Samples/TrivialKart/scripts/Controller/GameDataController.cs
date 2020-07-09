#define ONLINE
//#define OFFLINE

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Utils.Server;


public class GameDataController
{
    private const string FILE_NAME = "data.json";
    private static readonly string DATA_PATH = Application.persistentDataPath + "/" + FILE_NAME;

    private static GameData _gameData;

    public static void SaveGameData()
    {
#if OFFLINE
        SaveGameDataOffline();
#elif ONLINE
        SaveGameDataOnline();
#endif
    }

    private static void SaveGameDataOffline()
    {
        File.WriteAllText(DATA_PATH, JsonUtility.ToJson(_gameData, true));
    }

    private static void SaveGameDataOnline()
    {
        var values = new Dictionary<string, string>
        {
            ["gameData"] = JsonUtility.ToJson(_gameData)
        };
        sendUnityWebRequest(values, ServerURLs.SAVE_GAME_DATA_URL);
    }

    public static void LoadGameData()
    {
        Debug.Log(DATA_PATH);
#if OFFLINE
        LoadGameOffline();
#elif ONLINE
        LoadGameOnline();
#endif
    }

    private static void LoadGameOnline()
    {
        ServerResponseModel serverResponse =
            sendUnityWebRequest(new Dictionary<string, string>(), ServerURLs.GET_GAME_DATA_URL);
        if (serverResponse.success)
        {
            Debug.Log(serverResponse.result);
            _gameData = JsonUtility.FromJson<GameData>(serverResponse.result);
        }
        else
        {
            _gameData = new GameData();
        }
    }

    private static void LoadGameOffline()
    {
        try
        {
            // check if the data file exits
            if (File.Exists(DATA_PATH))
            {
                var contents = File.ReadAllText(DATA_PATH);
                _gameData = JsonUtility.FromJson<GameData>(contents);
                Debug.Log(contents);
            }
            else // if data file doesn't exist, create a default one
            {
                Debug.Log("Unable to read the save data, file does not exist");
                _gameData = new GameData();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public static GameData GetGameData()
    {
        return _gameData;
    }
}
