#define ONLINE
//#define OFFLINE

using System;
using System.IO;
using UnityEngine;

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
        NetworkRequestController.SaveGameDataOnline();
#endif
    }

    public static void LoadGameData()
    {
        Debug.Log(DATA_PATH);
#if OFFLINE
        LoadGameOffline();
#elif ONLINE
        NetworkRequestController.LoadGameOnline();
#endif
    }

    private static void SaveGameDataOffline()
    {
        File.WriteAllText(DATA_PATH, JsonUtility.ToJson(_gameData, true));
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

    public static void SetGameData(GameData gameData)
    {
        _gameData = gameData;
    }
}