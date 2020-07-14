using UnityEngine;
using UnityEngine.UI;

// controller for car store page
public class CarStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private Car _carToPurchaseObj;
    private readonly Color32 _lightGreyColor = new Color32(147, 147, 147, 255);



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
        if (GameDataController.GetGameData().coinsOwned >= CarList.CarTruck.Price)
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
            confirmText.text = "Would you like to purchase " + _carToPurchaseObj.CarName + " with $" +
                               _carToPurchaseObj.Price + "?";
        }
        else
        {
            confirmText.text = "Would you like to purchase " + _carToPurchaseObj.CarName + " with " +
                               _carToPurchaseObj.Price + " coins?";
        }
    }

    public void OnConfirmPurchaseButtonClicked()
    {
        // purchase APIs
        confirmPanel.SetActive(false);
        // if the item sales in coins
        if (!_carToPurchaseObj.IsPriceInDollar)
        {
            GameDataController.GetGameData().ReduceCoinsOwned((int) _carToPurchaseObj.Price);
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
        }
        
        PurchaseController.BuyProductId(_carToPurchaseObj.ProductId);
        // TODO: make the refresh happened after purchase
        RefreshPage();
    }

    public void OnCancelPurchaseButtonClicked()
    {
        confirmPanel.SetActive(false);
    }


    // TODO: need to decide to use CarObj or Car as the name.
    // check if the player own the car
    // if the player own the car, disable the interaction of the car item
    private void CheckCarOwnership(Car carObj)
    {
        var storeItemCarGameObj = carObj.StoreItemCarGameObj;
        if (GameDataController.GetGameData().CheckCarOwnership(carObj))
        {
            storeItemCarGameObj.GetComponent<Image>().color = _lightGreyColor;
            storeItemCarGameObj.GetComponent<Button>().interactable = false;
            storeItemCarGameObj.transform.Find("price").gameObject.GetComponent<Text>().text = "owned";
            if (!carObj.IsPriceInDollar)
            {
                storeItemCarGameObj.transform.Find("coinImage").gameObject.SetActive(false);
            }
        }
    }
}
