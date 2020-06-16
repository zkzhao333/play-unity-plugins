using System;
using UnityEngine;
using UnityEngine.UI;

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
    private void Awake()
    {
        _gas = car.GetComponent<Gas>();
        _gasLevelImage = gasLevelImageObj.GetComponent<Image>();
    }
    
   
    private void OnEnable()
    {
        RefreshGasStorePage();
    }
    
    // update the gas price and refresh the page when get into the gas store page
    private void RefreshGasStorePage()
    {
        _currentCost = Math.Ceiling((_gas.GetFullGasLevel() - _gas.GetGasLevel()) * 1);
        gasPrice.text = "* " + _currentCost;
        panelGasPrice.text = "Would you like to fill the gas tank with  " + _currentCost + "  coins";
        _gas.SetGasLevelHelper(_gasLevelImage, gasLevelImageObj);
        panelFillGas.SetActive(false);
        cannotAffordWarning.SetActive(false);
    }

    public void FillGasOnClick()
    {
        var currentCoins = PlayerPrefs.GetInt("coins", 20);
        if (currentCoins >= _currentCost)
        {
            panelFillGas.SetActive(true);
        }
        else
        {
            cannotAffordWarning.SetActive(true);
        }
    }
    
    public void CancelFillGas()
    {
        panelFillGas.SetActive(false);
    }

    public void ConfirmFillGas()
    {
        panelFillGas.SetActive(false);
        var currentCoins = PlayerPrefs.GetInt("coins", 20);
        if (currentCoins >= _currentCost)
        {
            PlayerPrefs.SetInt("coins", (int)(currentCoins - _currentCost));
            _gas.FilledGas();
            FindObjectOfType<GameManager>().SetCoins();
            FindObjectOfType<StoreController>().SetCoins();
            RefreshGasStorePage();
        }
    }
}
