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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for the tab/page switch in the store.
/// It switches pages and tabs when different tabs are clicked;
/// It updates the coin text indicator in the store pages;
/// </summary>
public class StoreController : MonoBehaviour
{
    public GameObject tab;
    public GameObject gasPage;
    public GameObject coinPage;
    public GameObject carPage;
    public GameObject subscriptionPage;
    public Text coinsCount;

    private const int UnselectedTabIndex = 0;
    private const int SelectedTabIndex = 1;
    private const int GasStorePageTabIndex = 0;
    private const int CoinStorePageTabIndex = 1;
    private const int CarStorePageTabIndex = 2;
    private const int SubscriptionPageTabIndex = 3;
    private GameObject[] _tabs;
    private List<GameObject> _storePages;
    
    // Start is called before the first frame update.
    private void Start()
    {
        _storePages = new List<GameObject>()
            {gasPage, coinPage, carPage, subscriptionPage};
        var tabsCount = tab.transform.childCount;
        _tabs = new GameObject[tabsCount];
        for (var tabIndex = 0; tabIndex < tabsCount; tabIndex++)
        {
            _tabs[tabIndex] = tab.transform.GetChild(tabIndex).gameObject;
        }
    }

    private void OnEnable()
    {
        // Update Coin text when enter the store.
        SetCoinsBasedOnGameData();
    }
    
    // TODO: Add parameters to the listeners.
    public void OnEnterGasPageButtonClicked()
    {
        SetPageActiveness(gasPage);
        SetTabActiveness(GasStorePageTabIndex);
    }

    public void OnEnterCoinPageButtonClicked()
    {
        SetPageActiveness(coinPage);
        SetTabActiveness(CoinStorePageTabIndex);
    }

    public void OnEnterCarPageButtonClicked()
    {
        SetPageActiveness(carPage);
        SetTabActiveness(CarStorePageTabIndex);
    }

    public void OnEnterSubscriptionPageButtonClicked()
    {
        SetPageActiveness(subscriptionPage);
        SetTabActiveness(SubscriptionPageTabIndex);
    }

    private void SetPageActiveness(GameObject targetPage)
    {
        foreach (var page in _storePages)
        {
            page.SetActive(page.Equals(targetPage));
        }
    }

    private void SetTabActiveness(int targetTabIndex)
    {
        // TODO: consider to make a class.
        for (var tabIndex = 0; tabIndex < _tabs.Length; tabIndex++)
        {
            var isTabSelected = tabIndex == targetTabIndex;
            _tabs[tabIndex].transform.GetChild(UnselectedTabIndex).gameObject.SetActive(!isTabSelected);
            _tabs[tabIndex].transform.GetChild(SelectedTabIndex).gameObject.SetActive(isTabSelected);
        }
    }

    // Update coin text in the store page.
    public void SetCoinsBasedOnGameData()
    {
        coinsCount.text = GameDataController.GetGameData().CoinsOwned.ToString();
    }
}
