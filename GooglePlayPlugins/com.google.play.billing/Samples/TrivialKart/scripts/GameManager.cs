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
    public Text coinsCount;

    private const string Filename = "data.json";
    private string _dataPath;
    private GameData _gameData;
    private List<GameObject> _canvasPagesList;

    // Init the game.
    public void Awake()
    {
        // TODO: User login.
        _dataPath = Application.persistentDataPath + "/" + Filename;
        Debug.Log(_dataPath);
        InitCarList();
        LoadGameData();
        SetCoins();
        _canvasPagesList = new List<GameObject>() {playPageCanvas, storePageCanvas, garagePageCanvas};
    }

    // Set the coins text at the play page.
    public void SetCoins()
    {
        coinsCount.text = _gameData.CoinsOwned.ToString();
    }

    // Switch pages when enter the store.
    public void OnEnterStoreButtonClicked()
    {
        SetCanvas(storePageCanvas);
    }

    // Switch pages when enter the play.
    public void OnEnterPlayPageButtonClicked()
    {
        SetCanvas(playPageCanvas);
    }

    // Switch pages when enter the garage.
    public void OnEnterGaragePageButtonClicked()
    {
        SetCanvas(garagePageCanvas);
    }

    private void SetCanvas(GameObject targetCanvasPage)
    {
        // Set all canvas pages to be inactive.
        foreach (var canvasPage in _canvasPagesList)
        {
            canvasPage.SetActive(false);
        }

        // Set the target canvas page to be active.
        targetCanvasPage.SetActive(true);
    }

    // Check if the player is in play mode (page).
    public bool IsInPlayPage()
    {
        return playPageCanvas.activeInHierarchy;
    }

    // Save game data.
    public void SaveGameData()
    {
        System.IO.File.WriteAllText(_dataPath, JsonUtility.ToJson(_gameData, true));
    }

    // TODO: put this part into the gamedata.cs.
    // Load game data.
    private void LoadGameData()
    {
        try
        {
            // Check if the data file exits.
            if (System.IO.File.Exists(_dataPath))
            {
                var contents = System.IO.File.ReadAllText(_dataPath);
                _gameData = JsonUtility.FromJson<GameData>(contents);
            }
            else // If data file doesn't exist, create a default one.
            {
                Debug.Log("Unable to read the save data, file does not exist");
                _gameData = new GameData();
                SaveGameData();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    // Get the game data.
    public GameData GetGameData()
    {
        return _gameData;
    }

    // Link car game obj to the car obj in carList.
    private void InitCarList()
    {
        // TODO: Improve it. 
        CarList.CarSedan.GarageItemGameObj = garageItemCarSedanGameObj;
        CarList.CarSedan.PlayCarGameObj = playCarSedanGameObj;
        CarList.CarSedan.StoreItemCarGameObj = storeItemCarSedanGameObj;
        CarList.CarTruck.GarageItemGameObj = garageItemCarTruckGameObj;
        CarList.CarTruck.PlayCarGameObj = playCarTruckGameObj;
        CarList.CarTruck.StoreItemCarGameObj = storeItemCarTruckGameObj;
        CarList.CarJeep.GarageItemGameObj = garageItemCarJeepGameObj;
        CarList.CarJeep.PlayCarGameObj = playCarJeepGameObj;
        CarList.CarJeep.StoreItemCarGameObj = storeItemCarJeepGameObj;
        CarList.CarKart.GarageItemGameObj = garageItemCarKartGameObj;
        CarList.CarKart.PlayCarGameObj = playCarKartGameObj;
        CarList.CarKart.StoreItemCarGameObj = storeItemCarKartGameObj;
    }
}