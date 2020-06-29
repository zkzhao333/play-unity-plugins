using UnityEngine;
using UnityEngine.UI;

// GameManager controls page switches among play, store and garage.
// It also manages game data loading and saving.
public class GameManager : MonoBehaviour
{
    public GameObject playPageCanvas;
    public GameObject storePageCanvas;
    public GameObject garagePageCanvas;
    public Text coinsCount;
    public Car sedan;

    private string _filename = "data.json";
    private string _dataPath;
    private GameData _gameData;

    // init the game
    public void Awake()
    {
        // user login
        _dataPath = Application.persistentDataPath + "/" + _filename;
        Debug.Log(_dataPath);
        LoadGameData();
        SetCoins();
    }

    // set the coins count at the play page
    public void SetCoins()
    {
        coinsCount.text = _gameData.coinOwned.ToString();
    }

    // switch pages when enter the store.
    public void EnterStore()
    {
        storePageCanvas.SetActive(true);
        playPageCanvas.SetActive(false);
        garagePageCanvas.SetActive(false);
    }

    // switch pages when enter the play.
    public void EnterPlayPage()
    {
        storePageCanvas.SetActive(false);
        playPageCanvas.SetActive(true);
        garagePageCanvas.SetActive(false);
    }

    // switch pages when enter the garage.
    public void EnterGaragePage()
    {
        storePageCanvas.SetActive(false);
        playPageCanvas.SetActive(false);
        garagePageCanvas.SetActive(true);
    }

    // check if the player is in play mode (page)
    public bool IsInPlayPage()
    {
        return playPageCanvas.activeInHierarchy;
    }

    // save game data
    public void SaveGameData()
    {
        System.IO.File.WriteAllText(_dataPath, JsonUtility.ToJson(_gameData, true));
    }

    // load game data
    private void LoadGameData()
    {
        try
        {
            // check if the data file exits
            if (System.IO.File.Exists(_dataPath))
            {
                string contents = System.IO.File.ReadAllText(_dataPath);
                _gameData = JsonUtility.FromJson<GameData>(contents);
            }
            else // if data file doesn't exist, create a default one
            {
                Debug.Log("Unable to read the save data, file does not exist");
                _gameData = new GameData(_dataPath);
                SaveGameData();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    // get the game data
    public GameData GetGameData()
    {
        return _gameData;
    }
}