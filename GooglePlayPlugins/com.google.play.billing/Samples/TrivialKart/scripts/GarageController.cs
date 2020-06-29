using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

public class GarageController : MonoBehaviour
{
    public GameObject itemSedan;
    public GameObject itemTruck;
    public GameObject itemJeep;
    public GameObject itemKart;
    public GameObject car;
    public Text coinsCount;

    private GameManager _gameManager;
    private GameData _gameData;
    private PlayerController _playerController;


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameData = _gameManager.GetGameData();
    }

    private void Start()
    {
        _playerController = car.GetComponent<PlayerController>();
    }

    // fresh the page when on eable
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
        CheckCarOwnershipHelper("carSedan", itemSedan);
        CheckCarOwnershipHelper("carTruck", itemTruck);
        CheckCarOwnershipHelper("carJeep", itemJeep);
        CheckCarOwnershipHelper("carKart", itemKart);
    }

    private void CheckCarOwnershipHelper(string carName, GameObject carObj)
    {
        if (_gameData.CheckOwnership(carName))
        {
            carObj.SetActive(true);
        }
    }

    private void CheckUsingStatus()
    {
        switch (_gameData.carInUse)
        {
            case "carSedan":
                SetUsingState(itemSedan, new List<GameObject> {itemTruck, itemJeep, itemKart});
                break;
            case "carTruck":
                SetUsingState(itemTruck, new List<GameObject> {itemSedan, itemJeep, itemKart});
                break;
            case "carJeep":
                SetUsingState(itemJeep, new List<GameObject> {itemSedan, itemTruck, itemKart});
                break;
            case "carKart":
                SetUsingState(itemKart, new List<GameObject> {itemSedan, itemTruck, itemJeep});
                break;
        }
    }

    private void SetUsingState(GameObject usingCarObj, List<GameObject> notUsingCarObjList)
    {
        usingCarObj.transform.Find("statusText").gameObject.SetActive(true);
        foreach (var carObj in notUsingCarObjList)
        {
            carObj.transform.Find("statusText").gameObject.SetActive(false);
        }
    }


    public void ItemSedanOnclick()
    {
        ItemCarsOnClick("carSedan");
    }

    public void ItemTruckOnClick()
    {
        ItemCarsOnClick("carTruck");
    }

    public void ItemJeepOnClick()
    {
        ItemCarsOnClick("carJeep");
    }

    public void ItemKartOnClick()
    {
        ItemCarsOnClick("carKart");
    }

    private void ItemCarsOnClick(string carName)
    {
        _gameData.carInUse = carName;
        RefreshPage();
        _gameManager.SaveGameData();
        _playerController.UpdateCarInUse();
    }

    private void SetCoins()
    {
        coinsCount.text = _gameData.coinOwned.ToString();
    }
}