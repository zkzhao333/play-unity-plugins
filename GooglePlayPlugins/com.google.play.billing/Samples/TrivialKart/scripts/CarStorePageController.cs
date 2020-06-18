using System;
using UnityEngine;
using UnityEngine.UI;

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
    
    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
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
        
        itemCoinsOnClick("carSedan", 0f, false);
        _itemNameToPurchase = "carSedan";
    }
    
    public void ItemTruckOnClick()
    {
        if (PlayerPrefs.GetInt("coins", 20) >= 20)
        {
            itemCoinsOnClick("Truck", 20f, false);
            _itemNameToPurchase = "carTruck";
        }
    }
    
    public void ItemJeepOnClick()
    {   
        itemCoinsOnClick("Jeep", 2.99f, true);
        _itemNameToPurchase = "carJeep";
    }
    
    public void ItemKartOnClick()
    {   
        itemCoinsOnClick("Kart", 4.99f, true);
        _itemNameToPurchase = "carKart";
    }

    private void itemCoinsOnClick(String carName, float price, bool dollarItem)
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
            _priceInCoin = (int)price;
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
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 20) - _priceInCoin);
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
        }
        
        //TODO play-billing-API
        bool confirmedPurchase = true;

        if (confirmedPurchase)
        {
            PlayerPrefs.SetInt(_itemNameToPurchase, 1);
            _gameManager.SaveData();
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
       
        if (PlayerPrefs.HasKey(carName))
        {
            carObj.GetComponent<Image>().color = new Color32(147, 147, 147, 255);
            carObj.transform.Find("price").gameObject.GetComponent<Text>().text = "owned";
            carObj.GetComponent<Button>().interactable = false;
            if (!dollarItem)
            {
                carObj.transform.Find("coinImage").gameObject.SetActive(false);
            }
        }
    }
}
