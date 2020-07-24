// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for car store page.
/// It listens to car purchase button click events,
/// initializing the purchase flow when car purchase button clicked;
/// It changes the UI of car store items based on ownerships.
/// </summary>
public class CarStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private CarList.Car _carToPurchaseObj;
    private readonly Color32 _lightGreyColor = new Color32(147, 147, 147, 255);
    
    private void OnEnable()
    {
        RefreshPage();
    }

    // Refresh the page.
    public void RefreshPage()
    {
        confirmPanel.SetActive(false);
        SetCarStoreItemStatusBasedOnOwnership();
        ApplyCoinItemsDiscount();
    }

    // Apply discounts on coin text for each car sales in coin.
    private void ApplyCoinItemsDiscount()
    {
        var discount = GameDataController.GetGameData().Discount;
        foreach (var car in CarList.List.Where(car => !car.IsRealMoneyPurchase))
        {
            car.StoreItemCarGameObj.transform.Find("price/priceValue").gameObject.GetComponent<Text>().text =
                (car.Price * discount).ToString(CultureInfo.InvariantCulture);
        }
    }
    
    public void OnItemSedanClicked()
    {
        _carToPurchaseObj = CarList.CarSedan;
        BuyCar();
    }

    public void OnItemTruckClicked()
    {
        var gameData = GameDataController.GetGameData();
        // players can purchase the coin item only it they have enough coin
        if (gameData.coinsOwned >= CarList.CarTruck.Price * gameData.Discount)
        {
            _carToPurchaseObj = CarList.CarTruck;
            BuyCar();
        }
        // TODO: Add a popup window if the player doesn't have enough coins.
    }

    public void OnItemJeepClicked()
    {
        _carToPurchaseObj = CarList.CarJeep;
        BuyCar();
    }

    public void OnItemKartClicked()
    {
        _carToPurchaseObj = CarList.CarKart;
        BuyCar();
    }

    private void BuyCar()
    {
        confirmPanel.SetActive(true);
        if (_carToPurchaseObj.IsRealMoneyPurchase)
        {
            confirmText.text = "Would you like to purchase " + _carToPurchaseObj.Name + " with $" +
                               _carToPurchaseObj.Price + "?";
        }
        else
        {
            confirmText.text = "Would you like to purchase " + _carToPurchaseObj.Name + " with " +
                               _carToPurchaseObj.Price * GameDataController.GetGameData().Discount + " coins?";
        }
    }

    public void OnConfirmPurchaseButtonClicked()
    {
        // purchase APIs
        confirmPanel.SetActive(false);
        // if the item sales in coins
        if (_carToPurchaseObj.IsRealMoneyPurchase)
        {
            PurchaseController.BuyProductId(_carToPurchaseObj.ProductId);
        }
        else
        {
            GameDataController.GetGameData().PurchaseCar(_carToPurchaseObj);
        }
    }

    public void OnCancelPurchaseButtonClicked()
    {
        confirmPanel.SetActive(false);
    }


    // TODO: need to decide to use CarObj or Car as the name.
    // check if the player own the car
    // if the player own the car, disable the interaction of the car item
    private void SetCarStoreItemStatusBasedOnOwnership()
    {
        foreach (var car in CarList.List)
        {
            var storeItemCarGameObj = car.StoreItemCarGameObj;
            if (GameDataController.GetGameData().CheckCarOwnership(car))
            {
                storeItemCarGameObj.GetComponent<Image>().color = _lightGreyColor;
                storeItemCarGameObj.GetComponent<Button>().interactable = false;
                storeItemCarGameObj.transform.Find("price").gameObject.GetComponent<Text>().text = "owned";
                SetDeferredPurchaseReminderActiveness(car, false);
                if (!car.IsRealMoneyPurchase)
                {
                    storeItemCarGameObj.transform.Find("coinImage").gameObject.SetActive(false);
                    storeItemCarGameObj.transform.Find("price/priceValue").gameObject.SetActive(false);
                }
            }
        }
       
    }

    public static void SetDeferredPurchaseReminderActiveness(CarList.Car car, bool isActive)
    {
        var storeItemCarGameObj = car.StoreItemCarGameObj;
        storeItemCarGameObj.transform.Find("deferredPurchaseReminder")?.gameObject.SetActive(isActive);
        // Set the Item not interactive if deferred purchase reminder is set up.
        storeItemCarGameObj.GetComponent<Button>().interactable = !isActive;

    }
}
