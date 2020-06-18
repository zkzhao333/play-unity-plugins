
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    public void Start()
    {
        PlayerPrefs.DeleteAll();
        // reset the coins when start the game
        PlayerPrefs.SetInt("coins", 20);
        SetCoins();
        _dataPath = Application.persistentDataPath + "/" + _filename;
        Debug.Log(_dataPath);
        LoadData();
    }
    
    // set the coins count at the play page
    public void SetCoins()
    {
        coinsCount.text = PlayerPrefs.GetInt("coins", 20).ToString();
    }

    public void EnterStore()
    {
        storePageCanvas.SetActive(true);
        playPageCanvas.SetActive(false);
        garagePageCanvas.SetActive(false);
    }

    public void EnterPlayPage()
    {
        storePageCanvas.SetActive(false);
        playPageCanvas.SetActive(true);
        garagePageCanvas.SetActive(false);
    }

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

    private void PrintCarJson()
    {
        sedan = new Car("sedan", 500, 0, false, true);
       
        print(JsonUtility.ToJson(sedan));
    }

    // save game data
    public void SaveData()
    {
        // update the ownership of the car
        foreach (var car in _gameData.cars)
        {
            // if playerpref has car name key, the user owned the car
            car.owned = PlayerPrefs.HasKey(car.carName);
            Debug.Log(car.carName);
        } 
        System.IO.File.WriteAllText(_dataPath, JsonUtility.ToJson(_gameData, true));
    }
    
    // load game data
    private void LoadData()
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
                _gameData = new GameData();
                _gameData.cars.Add(new Car("carSedan", 500, 0, false, true));
                _gameData.cars.Add(new Car("carTruck", 400, 0, false, false));
                _gameData.cars.Add(new Car("carJeep", 600, 0, true, false));
                _gameData.cars.Add(new Car("carKart", 1000, 0, true, false));
                SaveData();
            }
            
            // set car ownership into player pref
            foreach (var car in _gameData.cars)
            {
                if (car.owned)
                {
                    PlayerPrefs.SetInt(car.carName, 1);
                }
                Debug.Log(car.carName);
            }
            
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
