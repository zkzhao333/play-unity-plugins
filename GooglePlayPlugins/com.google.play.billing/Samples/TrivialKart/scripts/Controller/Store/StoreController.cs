using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controller for the tab/page switch in the store.
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
        SetCoins();
    }
    
    public void OnEnterGasPageButtonClicked()
    {
        SetPage(gasPage);
        SetTab(GasStorePageTabIndex);
    }

    public void OnEnterCoinPageButtonClicked()
    {
        SetPage(coinPage);
        SetTab(CoinStorePageTabIndex);
    }

    public void OnEnterCarPageButtonClicked()
    {
        SetPage(carPage);
        SetTab(CarStorePageTabIndex);
    }

    public void OnEnterSubscriptionPageButtonClicked()
    {
        SetPage(subscriptionPage);
        SetTab(SubscriptionPageTabIndex);
    }

    private void SetPage(GameObject targetPage)
    {
        // Set all store pages to inactive.
        foreach (var page in _storePages)
        {
            page.SetActive(false);
        }

        // Set the target page to active.
        targetPage.SetActive(true);
    }

    private void SetTab(int targetTagIndex)
    {
        // TODO: consider to make a class.
        // Set all tags to be unselected.
        foreach (var tab in _tabs)
        {
            tab.transform.GetChild(UnselectedTabIndex).gameObject.SetActive(true);
            tab.transform.GetChild(SelectedTabIndex).gameObject.SetActive(false);
        }

        // Set the target tag to be selected.
        _tabs[targetTagIndex].transform.GetChild(UnselectedTabIndex).gameObject.SetActive(false);
        _tabs[targetTagIndex].transform.GetChild(SelectedTabIndex).gameObject.SetActive(true);
    }

    // Update coin text in the store page.
    public void SetCoins()
    {
        coinsCount.text = GameDataController.GetGameData().CoinsOwned.ToString();
    }
}
