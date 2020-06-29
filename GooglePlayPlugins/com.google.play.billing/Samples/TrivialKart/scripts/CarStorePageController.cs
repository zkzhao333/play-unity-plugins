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
    private const int PriceInDollar = -1;
    private string _carNameToPurchase;
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
    
    private void OnEnable()
    {
        confirmPanel.SetActive(false);
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
        _carNameToPurchase = "carSedan";
        BuyCars();
    }

    public void ItemTruckOnClick()
    {
        // players can purchase the coin item only it they have enough coin
        if (_gameData.coinOwned >= CarList.CarTruck.price)
        {
            _carNameToPurchase = "carTruck";
            BuyCars();
        }
    }

    public void ItemJeepOnClick()
    {
        _carNameToPurchase = "carJeep";
        BuyCars();
    }

    public void ItemKartOnClick()
    {
        _carNameToPurchase = "carKart";
        BuyCars();
    }

    private void BuyCars()
    {
        var carObjToPurchase = CarList.GetCarByName(_carNameToPurchase);
        confirmPanel.SetActive(true);
        if (carObjToPurchase.isPriceInDollar)
        {
            // set price in coin to PriceInDollar if the car sales in dollar.
            _priceInCoin = PriceInDollar;
            confirmText.text = "Would you like to purchase " + carObjToPurchase.carName + " with $" +
                               carObjToPurchase.price + "?";
        }
        else
        {
            _priceInCoin = (int) carObjToPurchase.price;
            confirmText.text = "Would you like to purchase " + carObjToPurchase.carName + " with " +
                               carObjToPurchase.price + " coins?";
        }
    }

    public void ConfirmPurchase()
    {
        // purchase APIs
        confirmPanel.SetActive(false);
        // if the item sales in coins
        if (_priceInCoin != PriceInDollar)
        {
            _gameData.ReduceCoinsOwned(_priceInCoin);
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
        }

        //TODO play-billing-API
        bool confirmedPurchase = true;

        if (confirmedPurchase)
        {
            _gameData.PurchaseCar(_carNameToPurchase);
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
    private void CheckCarOwnership(string carName, GameObject carObj, bool isPriceInDollar)
    {
        if (_gameData.CheckOwnership(carName))
        {
            carObj.GetComponent<Image>().color = _lightGreyColor;
            carObj.GetComponent<Button>().interactable = false;
            carObj.transform.Find("price").gameObject.GetComponent<Text>().text = "owned";
            if (!isPriceInDollar)
            {
                carObj.transform.Find("coinImage").gameObject.SetActive(false);
            }
        }
    }
}