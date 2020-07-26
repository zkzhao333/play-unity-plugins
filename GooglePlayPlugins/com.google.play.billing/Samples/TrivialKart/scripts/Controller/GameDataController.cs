using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Controller for game data.
/// It controls game data loading, saving and fetching.
/// </summary>
public class GameDataController
{
    private const string FILE_NAME = "data.json";
    private static readonly string DATA_PATH = Application.persistentDataPath + "/" + FILE_NAME;

    private static GameData _gameData;

    public static void SaveGameData()
    {
#if ONLINE
        NetworkRequestController.SaveGameDataOnline();
#else
        SaveGameDataOffline();
#endif
    }

    public static void LoadGameData()
    {
#if ONLINE
        NetworkRequestController.LoadGameOnline();
#else
        Debug.Log(DATA_PATH);
        LoadGameOffline();
#endif
    }

    private static void SaveGameDataOffline()
    {
        File.WriteAllText(DATA_PATH, JsonUtility.ToJson(_gameData, true));
    }

    private static void LoadGameOffline()
    {
        Debug.Log("loading data");
        try
        {
            // Load data from data.json file if it exists and it's not empty.
            if (File.Exists(DATA_PATH) && new FileInfo( DATA_PATH ).Length != 0)
            {
                var contents = File.ReadAllText(DATA_PATH);
                _gameData = JsonUtility.FromJson<GameData>(contents);
                Debug.Log(contents);
            }
            else // If data file doesn't exist, create a default one.
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
        if (_gameData == null)
        {
            Debug.LogError("Game data has not been loaded yet.");
        }
        return _gameData;
    }

    public static void SetGameData(GameData gameData)
    {
        _gameData = gameData;
    }
}
