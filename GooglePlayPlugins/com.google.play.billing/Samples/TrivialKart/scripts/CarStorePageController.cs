using System;
using UnityEngine;
using UnityEngine.UI;

// controller for car store page
public class CarStorePageController : MonoBehaviour
{
    public GameObject itemSedan;
    public GameObject itemTruck;
    public GameObject itemJeep;
    public GameObject itemKart;
    public GameObject confirmPanel;
    public Text confirmText;

    private int _priceInCoin;
    private string _itemNameToPurchase;
    private GameManager _gameManager;
    private GameData _gameData;
    private readonly Color32 _lightGreyColor = new Color32(147, 147, 147, 255);

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameData = _gameManager.GetGameData();
        RefreshPage();
    }

    // refresh the page
    private void RefreshPage()
    {
        confirmPanel.SetActive(false);
        // check if player already owns the car
        CheckCarOwnership("carSedan", itemSedan, true);
        CheckCarOwnership("carTruck", itemTruck, false);
        CheckCarOwnership("carJeep", itemJeep, true);
        CheckCarOwnership("carKart", itemKart, true);
    }


    public void ItemSedanOnclick()
    {
        itemCarsOnClick("carSedan", 0f, false);
        _itemNameToPurchase = "carSedan";
    }

    public void ItemTruckOnClick()
    {
        // players can purchase the coin item only it they have enough coin
        if (_gameData.coinOwned>= 20)
        {
            itemCarsOnClick("Truck", 20f, false);
            _itemNameToPurchase = "carTruck";
        }
    }

    public void ItemJeepOnClick()
    {
        itemCarsOnClick("Jeep", 2.99f, true);
        _itemNameToPurchase = "carJeep";
    }

    public void ItemKartOnClick()
    {
        itemCarsOnClick("Kart", 4.99f, true);
        _itemNameToPurchase = "carKart";
    }

    private void itemCarsOnClick(String carName, float price, bool dollarItem) 
    {
        confirmPanel.SetActive(true);
        if (dollarItem)
        {
            // set price in coin to -1 if the car sales in dollar.
            _priceInCoin = -1;
            confirmText.text = "Would you like to purchase " + carName + " with $" + price + "?";
        }
        else
        {
            _priceInCoin = (int) price;
            confirmText.text = "Would you like to purchase " + carName + " with " + price + " coins?";
        }
    }

    public void ConfirmPurchase()
    {
        // purchase APIs
        confirmPanel.SetActive(false);
        // if the item sales in coins
        if (_priceInCoin != -1)
        {
            _gameData.ReduceCoinsOwned(_priceInCoin);
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
        }

        //TODO play-billing-API
        bool confirmedPurchase = true;

        if (confirmedPurchase)
        {
            _gameData.PurchaseCar(_itemNameToPurchase);
            _gameManager.SaveGameData();
        }

        RefreshPage();
    }

    public void CancelPurchase()
    {
        confirmPanel.SetActive(false);
    }


    // check if the player own the car
    // if the player own the car, disable the interaction of the car item
    private void CheckCarOwnership(String carName, GameObject carObj, bool dollarItem)
    {
        if (_gameData.CheckOwnership(carName))
        {
            carObj.GetComponent<Image>().color = _lightGreyColor;
            carObj.transform.Find("price").gameObject.GetComponent<Text>().text = "owned";
            carObj.GetComponent<Button>().interactable = false;
            if (!dollarItem)
            {
                carObj.transform.Find("coinImage").gameObject.SetActive(false);
            }
        }
    }
}