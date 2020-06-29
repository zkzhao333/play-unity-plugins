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
        BuyCoins(CoinList.FiveCoins);
    }

    public void TenCoinsOnClick()
    {
        BuyCoins(CoinList.TenCoins);
    }

    public void TwentyCoinsOnClick()
    {
        BuyCoins(CoinList.TwentyCoins);
    }

    public void FiftyCoinsOnClick()
    {
        BuyCoins(CoinList.FiftyCoins);
    }

    private void BuyCoins(CoinList.Coin coin)
    {
        confirmPanel.SetActive(true);
        _coinsToPurchase = coin.Amount;
        confirmText.text = "Would you like to purchase " + coin.Amount + "  Coins with $" + coin.Price + "?";
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