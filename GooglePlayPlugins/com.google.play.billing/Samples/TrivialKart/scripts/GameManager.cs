using System.Collections.Generic;
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
    public GameObject storeItemCarSedanGameObj;
    public GameObject storeItemCarTruckGameObj;
    public GameObject storeItemCarJeepGameObj;
    public GameObject storeItemCarKartGameObj;
    public GameObject garageItemCarSedanGameObj;
    public GameObject garageItemCarTruckGameObj;
    public GameObject garageItemCarJeepGameObj;
    public GameObject garageItemCarKartGameObj;
    public GameObject playCarSedanGameObj;
    public GameObject playCarJeepGameObj;
    public GameObject playCarTruckGameObj;
    public GameObject playCarKartGameObj;

    private const string Filename = "data.json";
    private string _dataPath;
    private GameData _gameData;
    private List<GameObject> _canvasPagesList;

    // init the game
    public void Awake()
    {
        // user login
        _dataPath = Application.persistentDataPath + "/" + Filename;
        Debug.Log(_dataPath);
        InitCarList();
        LoadGameData();
        SetCoins();
        _canvasPagesList = new List<GameObject>() {playPageCanvas, storePageCanvas, garagePageCanvas};
    }

    // set the coins count at the play page
    public void SetCoins()
    {
        coinsCount.text = _gameData.coinOwned.ToString();
    }

    // switch pages when enter the store.
    public void OnEnterStoreButtonClicked()
    {
        SetCanvas(storePageCanvas);
    }

    // switch pages when enter the play.
    public void OnEnterPlayPageButtonClicked()
    {
        SetCanvas(playPageCanvas);
    }

    // switch pages when enter the garage.
    public void OnEnterGaragePageButtonClicked()
    {
        SetCanvas(garagePageCanvas);
    }

    private void SetCanvas(GameObject targetCanvasPage)
    {
        // set all canvas pages to be inactive
        foreach (var canvasPage in _canvasPagesList)
        {
            canvasPage.SetActive(false);
        }

        // set the target canvas page to be active
        targetCanvasPage.SetActive(true);
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
                var contents = System.IO.File.ReadAllText(_dataPath);
                _gameData = JsonUtility.FromJson<GameData>(contents);
            }
            else // if data file doesn't exist, create a default one
            {
                Debug.Log("Unable to read the save data, file does not exist");
                _gameData = new GameData(_dataPath);
                SaveGameData();
            }

            // transfer the stored carName to carObj for future use
            _gameData.SetCarObjInUse();
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

    // link car game obj to the car obj in carList
    private void InitCarList()
    {
        CarList.CarSedan.garageItemGameObj = garageItemCarSedanGameObj;
        CarList.CarSedan.playItemGameObj = playCarSedanGameObj;
        CarList.CarSedan.storeItemCarGameObj = storeItemCarSedanGameObj;
        CarList.CarTruck.garageItemGameObj = garageItemCarTruckGameObj;
        CarList.CarTruck.playItemGameObj = playCarTruckGameObj;
        CarList.CarTruck.storeItemCarGameObj = storeItemCarTruckGameObj;
        CarList.CarJeep.garageItemGameObj = garageItemCarJeepGameObj;
        CarList.CarJeep.playItemGameObj = playCarJeepGameObj;
        CarList.CarJeep.storeItemCarGameObj = storeItemCarJeepGameObj;
        CarList.CarKart.garageItemGameObj = garageItemCarKartGameObj;
        CarList.CarKart.playItemGameObj = playCarKartGameObj;
        CarList.CarKart.storeItemCarGameObj = storeItemCarKartGameObj;
    }
}