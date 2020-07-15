using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// controller for car store page
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

    // refresh the page
    public void RefreshPage()
    {
        confirmPanel.SetActive(false);
        CheckCarOwnerships();
        SetCoinItemPrices();
    }

    // Apply discounts on coin text for each car sales in coin
    private void SetCoinItemPrices()
    {
        var discount = GameDataController.GetGameData().Discount;
        foreach (var car in CarList.List.Where(car => !car.IsPriceInDollar))
        {
            car.StoreItemCarGameObj.transform.Find("price/priceValue").gameObject.GetComponent<Text>().text =
                (car.Price * discount).ToString(CultureInfo.InvariantCulture);
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
        if (GameDataController.GetGameData().coinsOwned >= CarList.CarTruck.Price * GameDataController.GetGameData().Discount)
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
        if (_carToPurchaseObj.IsPriceInDollar)
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
        if (!_carToPurchaseObj.IsPriceInDollar)
        {
            GameDataController.GetGameData().PurchaseCar(_carToPurchaseObj);
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
        }
        
        // TODO: OOD.
        PurchaseController.BuyProductId(_carToPurchaseObj.ProductId);
        // TODO: make the refresh happened after purchase
    }

    public void OnCancelPurchaseButtonClicked()
    {
        confirmPanel.SetActive(false);
    }


    // TODO: need to decide to use CarObj or Car as the name.
    // check if the player own the car
    // if the player own the car, disable the interaction of the car item
    private void CheckCarOwnerships()
    {
        foreach (var car in CarList.List)
        {
            var storeItemCarGameObj = car.StoreItemCarGameObj;
            if (GameDataController.GetGameData().CheckCarOwnership(car))
            {
                storeItemCarGameObj.GetComponent<Image>().color = _lightGreyColor;
                storeItemCarGameObj.GetComponent<Button>().interactable = false;
                storeItemCarGameObj.transform.Find("price").gameObject.GetComponent<Text>().text = "owned";
                if (!car.IsPriceInDollar)
                {
                    storeItemCarGameObj.transform.Find("coinImage").gameObject.SetActive(false);
                    storeItemCarGameObj.transform.Find("price/priceValue").gameObject.SetActive(false);
                }
            }
        }
       
    }
}
