using System;
using UnityEngine;
using UnityEngine.UI;

// controller for the gas store page
public class GasStorePageController : MonoBehaviour
{
    public Text gasPrice;
    public Text panelGasPrice;
    public GameObject panelFillGas;
    public GameObject gasLevelImageObj;
    public GameObject cannotAffordWarning;
    public GameObject car; // need to get gas class from car

    private double _currentCost;
    private Gas _gas;
    private Image _gasLevelImage;
    private GameData _gameData;

    private void Awake()
    {
        _gas = car.GetComponent<Gas>();
        _gasLevelImage = gasLevelImageObj.GetComponent<Image>();
        _gameData = FindObjectOfType<GameManager>().GetGameData();
    }

    private void OnEnable()
    {
        RefreshGasStorePage();
    }

    // update the gas price and refresh the page when get into the gas store page
    private void RefreshGasStorePage()
    {
        _currentCost = Math.Ceiling((Gas.GetFullGasLevel() - _gas.GetGasLevel()) * _gameData.Discount) ;
        gasPrice.text = "* " + _currentCost;
        panelGasPrice.text = "Would you like to fill the gas tank with  " + _currentCost + "  coins";
        _gas.SetGasLevelHelper(_gasLevelImage, gasLevelImageObj);
        panelFillGas.SetActive(false);
        cannotAffordWarning.SetActive(false);
    }

    // listener for fill gas button.
    public void OnFillGasButtonClicked()
    {
        var currentCoins = _gameData.CoinsOwned;
        if (currentCoins >= _currentCost)
        {
            panelFillGas.SetActive(true);
        }
        else
        {
            cannotAffordWarning.SetActive(true);
        }
    }

    // Listener for cancel/no fill gas button.
    public void OnCancelFillGasButtonClicked()
    {
        panelFillGas.SetActive(false);
    }

    // Listener for confirm/yes fill gas button.
    public void OnConfirmFillGasButtonClicked()
    {
        panelFillGas.SetActive(false);
        var currentCoins = _gameData.CoinsOwned;
        if (currentCoins >= _currentCost)
        {
            _gameData.ReduceCoinsOwned((int) _currentCost);
            _gas.FilledGas();
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
            RefreshGasStorePage();
        }
    }
}