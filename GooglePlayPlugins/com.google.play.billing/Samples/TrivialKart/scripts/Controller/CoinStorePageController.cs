using UnityEngine;
using UnityEngine.UI;

// Controller for coin store page
public class CoinStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private CoinList.Coin _coinToPurchase;
    private GameData _gameData;

    private void Start()
    {
        _gameData = FindObjectOfType<GameManager>().GetGameData();
    }

    private void OnEnable()
    {
        confirmPanel.SetActive(false);
    }

    public void OnFiveCoinsClicked()
    {
        _coinToPurchase = CoinList.FiveCoins;
        BuyCoins();
    }

    public void OnTenCoinsClicked()
    {
        _coinToPurchase = CoinList.TenCoins;
        BuyCoins();
    }

    public void OnTwentyCoinsClicked()
    {
        _coinToPurchase = CoinList.TwentyCoins;
        BuyCoins();
    }

    public void OnFiftyCoinsClicked()
    {
        _coinToPurchase = CoinList.FiftyCoins;
        BuyCoins();
    }

    private void BuyCoins()
    {
        confirmPanel.SetActive(true);
        confirmText.text = "Would you like to purchase " + _coinToPurchase.Amount + "  Coins with $" +
                           _coinToPurchase.Price + "?";
    }

    public void OnConfirmPurchaseButtonClicked()
    {
        // Purchase APIs here
        confirmPanel.SetActive(false);
        _gameData.IncreaseCoinsOwned(_coinToPurchase.Amount);
        FindObjectOfType<GameManager>().SetCoins();
        FindObjectOfType<StoreController>().SetCoins();
    }

    public void OnCancelPurchaseButtonClicked()
    {
        confirmPanel.SetActive(false);
    }
}