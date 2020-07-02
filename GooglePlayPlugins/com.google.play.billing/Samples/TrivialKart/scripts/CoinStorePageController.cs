using UnityEngine;
using UnityEngine.UI;

// Controller for coin store page
public class CoinStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private CoinList.Coin _coinToPurchaseObj;
    private GameData _gameData;

    private void Start()
    {
        _gameData = FindObjectOfType<GameManager>().GetGameData();
    }

    public void OnFiveCoinsClicked()
    {
        _coinToPurchaseObj = CoinList.FiveCoins;
        BuyCoins();
    }

    public void OnTenCoinsClicked()
    {
        _coinToPurchaseObj = CoinList.TenCoins;
        BuyCoins();
    }

    public void OnTwentyCoinsClicked()
    {
        _coinToPurchaseObj = CoinList.TwentyCoins;
        BuyCoins();
    }

    public void OnFiftyCoinsClicked()
    {
        _coinToPurchaseObj = CoinList.FiftyCoins;
        BuyCoins();
    }

    private void BuyCoins()
    {
        confirmPanel.SetActive(true);
        confirmText.text = "Would you like to purchase " + _coinToPurchaseObj.Amount + "  Coins with $" +
                           _coinToPurchaseObj.Price + "?";
    }

    public void OnConfirmPurchaseButtonClicked()
    {
        // purchase APIs
        confirmPanel.SetActive(false);
        _gameData.IncreaseCoinsOwned(_coinToPurchaseObj.Amount);
        FindObjectOfType<GameManager>().SetCoins();
        FindObjectOfType<StoreController>().SetCoins();
    }

    public void OnCancelPurchaseButtonClicked()
    {
        confirmPanel.SetActive(false);
    }

    private void OnEnable()
    {
        confirmPanel.SetActive(false);
    }
}