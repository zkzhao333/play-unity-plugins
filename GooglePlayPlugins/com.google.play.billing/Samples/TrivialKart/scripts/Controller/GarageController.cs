using UnityEngine;
using UnityEngine.UI;

// controller of the garage page
public class GarageController : MonoBehaviour
{
    public GameObject playCarGameObject;
    public Text coinsCountText;

    private GameManager _gameManager;
    private GameData _gameData;
    private PlayerController _playerController;


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameData = _gameManager.GetGameData();
        _playerController = playCarGameObject.GetComponent<PlayerController>();
    }


    // fresh the page when on enable
    private void OnEnable()
    {
        RefreshPage();
    }

    private void RefreshPage()
    {
        // check if player already owns the car
        CheckCarOwnership();
        CheckUsingStatus();
        SetCoins();
    }

    private void CheckCarOwnership()
    {
        foreach (var car in CarList.List)
        {
            var isCarOwned = _gameData.CheckOwnership(car.CarName);
            // set the car item active if the player owns the car.
            car.GarageItemGameObj.SetActive(isCarOwned);
        }
    }

    private void CheckUsingStatus()
    {
        foreach (var carObj in CarList.List)
        {
            carObj.GarageItemGameObj.transform.Find("statusText").gameObject.SetActive(false);
        }

        _gameData.CarInUseObj.GarageItemGameObj.transform.Find("statusText").gameObject.SetActive(true);
    }


    public void OnItemSedanClicked()
    {
        SwitchCarInUse(CarList.CarSedan);
    }

    public void OnItemTruckClicked()
    {
        SwitchCarInUse(CarList.CarTruck);
    }

    public void OnItemJeepClicked()
    {
        SwitchCarInUse(CarList.CarJeep);
    }

    public void OnItemKartClicked()
    {
        SwitchCarInUse(CarList.CarKart);
    }

    private void SwitchCarInUse(Car targetCar)
    {
        // TODO: combine the save game data into the _gameData
        _gameData.ChangeCar(targetCar);
        _gameManager.SaveGameData();
        RefreshPage();
        _playerController.UpdateCarInUse();
    }

    private void SetCoins()
    {
        coinsCountText.text = _gameData.CoinsOwned.ToString();
    }
}