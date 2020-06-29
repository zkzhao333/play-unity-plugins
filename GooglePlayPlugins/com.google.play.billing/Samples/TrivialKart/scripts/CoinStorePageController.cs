using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private int _coinsToPurchase;
    private GameData _gameData;

    private void Start()
    {
        _gameData = FindObjectOfType<GameManager>().GetGameData();
    }

    public void FiveCoinsOnClick()
    {
        itemCoinsOnClick(5, 0.99f);
    }

    public void TenCoinsOnClick()
    {
        itemCoinsOnClick(10, 1.99f);
    }

    public void TwentyCoinsOnClick()
    {
        itemCoinsOnClick(20, 2.99f);
    }

    public void FiftyCoinsOnClick()
    {
        itemCoinsOnClick(50, 4.99f);
    }

    private void itemCoinsOnClick(int coinAmount, float price)
    {
        confirmPanel.SetActive(true);
        _coinsToPurchase = coinAmount;
        confirmText.text = "Would you like to purchase " + coinAmount + "    Coins with $" + price + "?";
    }

    public void ConfirmPurchase()
    {
        // purchase APIs
        confirmPanel.SetActive(false);
        _gameData.IncreaseCoinsOwned(_coinsToPurchase);
        FindObjectOfType<GameManager>().SetCoins();
        FindObjectOfType<StoreController>().SetCoins();
    }

    public void CancelPurchase()
    {
        confirmPanel.SetActive(false);
    }

    private void OnEnable()
    {
        confirmPanel.SetActive(false);
    }
}