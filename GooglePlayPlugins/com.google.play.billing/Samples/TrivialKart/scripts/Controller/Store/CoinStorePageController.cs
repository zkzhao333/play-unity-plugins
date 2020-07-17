using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for coin store page.
/// It listens to coin purchase button click events,
/// initializing the purchase flow when coin purchase button clicked.
/// </summary>
public class CoinStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private CoinList.Coin _coinToPurchase;

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
        confirmPanel.SetActive(false);
        PurchaseController.BuyProductId(_coinToPurchase.ProductId);
    }

    public void OnCancelPurchaseButtonClicked()
    {
        confirmPanel.SetActive(false);
    }
}