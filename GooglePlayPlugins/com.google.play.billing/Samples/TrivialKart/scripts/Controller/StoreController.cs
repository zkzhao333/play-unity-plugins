using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controller for the whole store.
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
    private int _tabsCount;
    private GameData _gameData;
    private List<GameObject> _storePages;

    private void Awake()
    {
        _gameData = FindObjectOfType<GameManager>().GetGameData();
    }

    // Start is called before the first frame update.
    private void Start()
    {
        _storePages = new List<GameObject>()
            {gasPage, coinPage, carPage, subscriptionPage};
        _tabsCount = tab.transform.childCount;
        _tabs = new GameObject[_tabsCount];
        for (var tabIndex = 0; tabIndex < _tabsCount; tabIndex++)
        {
            _tabs[tabIndex] = tab.transform.GetChild(tabIndex).gameObject;
        }
    }

    private void Update()
    {
        // Keep the coin text updated.
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
        for (var tagIndex = 0; tagIndex < _tabsCount; tagIndex++)
        {
            _tabs[tagIndex].transform.GetChild(UnselectedTabIndex).gameObject.SetActive(true);
            _tabs[tagIndex].transform.GetChild(SelectedTabIndex).gameObject.SetActive(false);
        }

        // Set the target tag to be selected.
        _tabs[targetTagIndex].transform.GetChild(UnselectedTabIndex).gameObject.SetActive(false);
        _tabs[targetTagIndex].transform.GetChild(SelectedTabIndex).gameObject.SetActive(true);
    }

    // Update coin text.
    public void SetCoins()
    {
        coinsCount.text = _gameData.CoinsOwned.ToString();
    }
}