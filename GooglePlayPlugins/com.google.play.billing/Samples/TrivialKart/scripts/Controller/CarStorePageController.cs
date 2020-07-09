using UnityEngine;
using UnityEngine.UI;

// controller for car store page
public class CarStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private Car _carToPurchaseObj;
    private GameManager _gameManager;
    private GameData _gameData;
    private readonly Color32 _lightGreyColor = new Color32(147, 147, 147, 255);

    // Start is called before the first frame update
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameData = GameDataController.GetGameData();
    }

    private void OnEnable()
    {
        RefreshPage();
    }

    // refresh the page
    private void RefreshPage()
    {
        confirmPanel.SetActive(false);
        // check if the player already owns the car
        foreach (var car in CarList.List)
        {
            CheckCarOwnership(car);
        }
    }


    public void OnItemSedanClicked()
    {
        _carToPurchaseObj = CarList.CarSedan;
        BuyCars();
    }

    public void OnItemTruckClicked()
    {
        // players can purchase the coin item only it they have enough coin
        if (_gameData.coinsOwned >= CarList.CarTruck.price)
        {
            _carToPurchaseObj = CarList.CarTruck;
            BuyCars();
        }
    }

    public void OnItemJeepClicked()
    {
        _carToPurchaseObj = CarList.CarJeep;
        BuyCars();
    }

    public void OnItemKartClicked()
    {
        _carToPurchaseObj = CarList.CarKart;
        BuyCars();
    }

    private void BuyCars()
    {
        confirmPanel.SetActive(true);
        if (_carToPurchaseObj.isPriceInDollar)
        {
            confirmText.text = "Would you like to purchase " + _carToPurchaseObj.carName + " with $" +
                               _carToPurchaseObj.price + "?";
        }
        else
        {
            confirmText.text = "Would you like to purchase " + _carToPurchaseObj.carName + " with " +
                               _carToPurchaseObj.price + " coins?";
        }
    }

    public void OnConfirmPurchaseButtonClicked()
    {
        // purchase APIs
        confirmPanel.SetActive(false);
        // if the item sales in coins
        if (!_carToPurchaseObj.isPriceInDollar)
        {
            _gameData.ReduceCoinsOwned((int) _carToPurchaseObj.price);
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
        }

        //TODO play-billing-API
        bool confirmedPurchase = true;

        if (confirmedPurchase)
        {
            _gameData.PurchaseCar(_carToPurchaseObj.carName);
        }

        RefreshPage();
    }

    public void OnCancelPurchaseButtonClicked()
    {
        confirmPanel.SetActive(false);
    }


    // check if the player own the car
    // if the player own the car, disable the interaction of the car item
    private void CheckCarOwnership(Car carObj)
    {
        var storeItemCarGameObj = carObj.storeItemCarGameObj;
        if (_gameData.CheckOwnership(carObj.carName))
        {
            storeItemCarGameObj.GetComponent<Image>().color = _lightGreyColor;
            storeItemCarGameObj.GetComponent<Button>().interactable = false;
            storeItemCarGameObj.transform.Find("price").gameObject.GetComponent<Text>().text = "owned";
            if (!carObj.isPriceInDollar)
            {
                storeItemCarGameObj.transform.Find("coinImage").gameObject.SetActive(false);
            }
        }
    }
}
