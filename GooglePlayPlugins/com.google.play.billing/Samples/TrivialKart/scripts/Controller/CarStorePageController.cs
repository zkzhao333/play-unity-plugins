using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

// Controller for car store page.
public class CarStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private readonly Color32 _lightGreyColor = new Color32(147, 147, 147, 255);
    private Car _carToPurchase;
    private GameManager _gameManager;
    private GameData _gameData;


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameData = _gameManager.GetGameData();
    }

    private void OnEnable()
    {
        RefreshPage();
    }

    // Refresh the page.
    private void RefreshPage()
    {
        confirmPanel.SetActive(false);
        CheckCarOwnerships();
        UpdateCarPrice();
    }

    // Check if the player own the car.
    // If the player own the car, disable the interaction of the car store item.
    private void CheckCarOwnerships()
    {
        foreach (var carObj in CarList.List)
        {
            var storeItemCarGameObj = carObj.StoreItemCarGameObj;
            if (!_gameData.CheckOwnership(carObj.CarName)) continue;
            storeItemCarGameObj.GetComponent<Image>().color = _lightGreyColor;
            storeItemCarGameObj.GetComponent<Button>().interactable = false;
            storeItemCarGameObj.transform.Find("price").gameObject.GetComponent<Text>().text = "owned";
            if (!carObj.IsPriceInDollar)
            {
                storeItemCarGameObj.transform.Find("coinImage").gameObject.SetActive(false);
            }
        }
    }

    // Apply potential discounts on all coin items.
    private void UpdateCarPrice()
    {
        foreach (var carObj in CarList.List)
        {
            if (carObj.IsPriceInDollar) continue;
            var priceText = carObj.StoreItemCarGameObj.transform.Find("price").gameObject.transform.Find("priceValue")
                .gameObject.GetComponent<Text>();
            
            var newPrice = Math.Floor(carObj.Price * _gameData.Discount);
            priceText.text = newPrice.ToString(CultureInfo.InvariantCulture);
        }
    }


    public void OnItemSedanClicked()
    {
        _carToPurchase = CarList.CarSedan;
        BuyCars();
    }

    public void OnItemTruckClicked()
    {
        // Players can purchase the coin item only it they have enough coin.
        if (_gameData.CoinsOwned >= CarList.CarTruck.Price)
        {
            _carToPurchase = CarList.CarTruck;
            BuyCars();
        }
    }

    public void OnItemJeepClicked()
    {
        _carToPurchase = CarList.CarJeep;
        BuyCars();
    }

    public void OnItemKartClicked()
    {
        _carToPurchase = CarList.CarKart;
        BuyCars();
    }

    private void BuyCars()
    {
        confirmPanel.SetActive(true);
        if (_carToPurchase.IsPriceInDollar)
        {
            confirmText.text = "Would you like to purchase " + _carToPurchase.CarName + " with $" +
                               _carToPurchase.Price + "?";
        }
        else
        {
            confirmText.text = "Would you like to purchase " + _carToPurchase.CarName + " with " +
                               _carToPurchase.Price * _gameData.Discount + " coins?";
        }
    }

    public void OnConfirmPurchaseButtonClicked()
    {
        // Purchase APIs will be applied here.
        confirmPanel.SetActive(false);
        // If the item sales in coins.
        if (!_carToPurchase.IsPriceInDollar)
        {
            _gameData.PurchaseCar(_carToPurchase);
            // TODO: Combine the following three methods to gameData controller in the future. 
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
            _gameManager.SaveGameData();
            return;
        }

        // TODO: Play-billing-API will be applied here.
        bool confirmedPurchase = true;

        if (confirmedPurchase)
        {
            _gameData.PurchaseCar(_carToPurchase);
            _gameManager.SaveGameData();
        }

        RefreshPage();
    }

    public void OnCancelPurchaseButtonClicked()
    {
        confirmPanel.SetActive(false);
    }
}