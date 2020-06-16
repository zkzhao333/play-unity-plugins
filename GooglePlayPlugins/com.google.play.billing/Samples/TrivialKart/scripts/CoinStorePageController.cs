using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private int _coinsToPurchase;

    public void FiveCoinsOnClick()
    {   
        confirmPanel.SetActive(true);
        _coinsToPurchase = 5;
        confirmText.text = "Would you like to purchase 5     Coins with $0.99?";
    }
    
    public void TenCoinsOnClick()
    {   
        confirmPanel.SetActive(true);
        _coinsToPurchase = 10;
        confirmText.text = "Would you like to purchase 10  Coins with $1.99?";
    }
    
    public void TwentyCoinsOnClick()
    {   
        confirmPanel.SetActive(true);
        _coinsToPurchase = 20;
        confirmText.text = "Would you like to purchase 20  Coins with $2.99?";
    }
    
    public void FiftyCoinsOnClick()
    {   
        confirmPanel.SetActive(true);
        _coinsToPurchase = 50;
        confirmText.text = "Would you like to purchase 50  Coins with $4.99?";
    }

    public void ConfirmPurchase()
    {
        // purchase APIs
        confirmPanel.SetActive(false);
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 20) + _coinsToPurchase);
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
