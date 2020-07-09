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

    private List<GameObject> _canvasPagesList;

    // init the game
    public void Awake()
    {
        // user login
        NetworkRequestController.registerUserDevice();
        InitCarList();
        GameDataController.LoadGameData();
        SetCoins();
        _canvasPagesList = new List<GameObject>() {playPageCanvas, storePageCanvas, garagePageCanvas};
    }

    // set the coins count at the play page
    public void SetCoins()
    {
        coinsCount.text = GameDataController.GetGameData().coinsOwned.ToString();
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

    // link car game obj to the car obj in carList
    private void InitCarList()
    {
        CarList.CarSedan.garageItemGameObj = garageItemCarSedanGameObj;
        CarList.CarSedan.playCarGameObj = playCarSedanGameObj;
        CarList.CarSedan.storeItemCarGameObj = storeItemCarSedanGameObj;
        CarList.CarTruck.garageItemGameObj = garageItemCarTruckGameObj;
        CarList.CarTruck.playCarGameObj = playCarTruckGameObj;
        CarList.CarTruck.storeItemCarGameObj = storeItemCarTruckGameObj;
        CarList.CarJeep.garageItemGameObj = garageItemCarJeepGameObj;
        CarList.CarJeep.playCarGameObj = playCarJeepGameObj;
        CarList.CarJeep.storeItemCarGameObj = storeItemCarJeepGameObj;
        CarList.CarKart.garageItemGameObj = garageItemCarKartGameObj;
        CarList.CarKart.playCarGameObj = playCarKartGameObj;
        CarList.CarKart.storeItemCarGameObj = storeItemCarKartGameObj;
    }

    void OnApplicationPause()
    {
        GameDataController.SaveGameData();
    }
}
