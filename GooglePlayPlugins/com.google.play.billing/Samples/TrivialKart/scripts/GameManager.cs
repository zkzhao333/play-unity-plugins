using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GameManager inits the game when the game starts and controls the play canvas.
/// It inits constant data, requests for game data load;
/// It controls canvas switches among play, store and garage;
/// It updates the coin text indicator in the play canvas.
/// </summary>
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
    public GameObject silverVipSubscribeButtonGameObj;
    public GameObject goldenVipSubscribeButtonGameObj;
    public Text coinsCount;

    private List<GameObject> _canvasPagesList;
    
    // Init the game.
    public void Awake()
    {
#if ONLINE
        NetworkRequestController.registerUserDevice();
#endif
        InitConstantData();
        GameDataController.LoadGameData();
        SetCoins();
        SetCanvas(playPageCanvas);
        // TODO: Set the background when start the game.
    }

    // Set the coins text at the play page.
    public void SetCoins()
    {
        coinsCount.text = GameDataController.GetGameData().coinsOwned.ToString();
        
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

    // Init constant game data before the game starts.
    private void InitConstantData()
    {
        InitCarList();
        InitBackGroundList();
        InitSubscriptionList();
        _canvasPagesList = new List<GameObject>() {playPageCanvas, storePageCanvas, garagePageCanvas};
    }
    
    // Link car game object to the car object in carList.
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
    
    // Link background game object to the background object in backgroundList.
    private void InitBackGroundList()
    {
        BackgroundList.BlueGrassBackground.GarageItemGameObj =   GameObject.FindWithTag("garagePages").transform.Find("backGroundPage/blueGrassBackground").gameObject;
        BackgroundList.BlueGrassBackground.ImageSprite = Resources.Load<Sprite>("background/blueGrass");
        BackgroundList.MushroomBackground.GarageItemGameObj =  GameObject.FindWithTag("garagePages").transform.Find("backGroundPage/mushroomBackground").gameObject;
        BackgroundList.MushroomBackground.ImageSprite = Resources.Load<Sprite>("background/coloredShroom");
    }

    private void InitSubscriptionList()
    {
        SubscriptionList.SilverSubscription.SubscribeButton = silverVipSubscribeButtonGameObj;
        SubscriptionList.GoldenSubscription.SubscribeButton = goldenVipSubscribeButtonGameObj;
        SubscriptionList.NoSubscription.SubscribeButton = new GameObject();
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        GameDataController.SaveGameData();
    }

    private void OnApplicationQuit()
    {
        GameDataController.SaveGameData();
    }
}
