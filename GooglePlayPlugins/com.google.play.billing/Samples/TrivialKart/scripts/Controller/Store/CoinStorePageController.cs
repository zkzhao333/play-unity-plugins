// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
    
    public static void SetDeferredPurchaseReminderActiveness(CoinList.Coin coin, bool isActive)
    {
        var storeItemCoinGameObj = coin.StoreItemCoinGameObj;
        storeItemCoinGameObj.transform.Find("deferredPurchaseReminder")?.gameObject.SetActive(isActive);
        storeItemCoinGameObj.GetComponent<Button>().interactable = !isActive;
    }
}