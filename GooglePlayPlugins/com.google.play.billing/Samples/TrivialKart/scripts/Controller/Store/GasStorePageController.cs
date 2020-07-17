using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for the gas store page.
/// It listens to the fill gas button click event. 
/// </summary>
public class GasStorePageController : MonoBehaviour
{
    public Text gasPrice;
    public Text panelGasPrice;
    public GameObject panelFillGas;
    public GameObject gasLevelImageObj;
    public GameObject cannotAffordWarning;
    public GameObject car; 

    private double _currentCost;
    private Gas _gas;
    private Image _gasLevelImage;

    private void Awake()
    {
        _gas = car.GetComponent<Gas>();
        _gasLevelImage = gasLevelImageObj.GetComponent<Image>();
    }

    // Update the gas price and refresh the page when get into the gas store page.
    private void OnEnable()
    {
        RefreshGasStorePage();
    }
    
    private void RefreshGasStorePage()
    {
        _currentCost = Math.Ceiling((Gas.FullGasLevel - _gas.GasLevel) * GameDataController.GetGameData().Discount) ;
        gasPrice.text = "* " + _currentCost;
        panelGasPrice.text = "Would you like to fill the gas tank with  " + _currentCost + "  coins";
        _gas.SetGasLevelHelper(_gasLevelImage, gasLevelImageObj);
        panelFillGas.SetActive(false);
        cannotAffordWarning.SetActive(false);
    }
    
    public void OnFillGasButtonClicked()
    {
        var currentCoins = GameDataController.GetGameData().CoinsOwned;
        if (currentCoins >= _currentCost)
        {
            panelFillGas.SetActive(true);
        }
        else
        {
            cannotAffordWarning.SetActive(true);
        }
    }
    
    public void OnCancelFillGasButtonClicked()
    {
        panelFillGas.SetActive(false);
    }

    // Listener for confirm/yes fill gas button.
    public void OnConfirmFillGasButtonClicked()
    {
        panelFillGas.SetActive(false);
        var currentCoins = GameDataController.GetGameData().CoinsOwned;
        if (currentCoins >= _currentCost)
        {
            GameDataController.GetGameData().ReduceCoinsOwned((int) _currentCost);
            _gas.FilledGas();
            RefreshGasStorePage();
        }
    }
}
