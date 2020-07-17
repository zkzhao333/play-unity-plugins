using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for the tab/page switch in the garage.
/// It switches pages and tabs when different tabs are clicked;
/// It updates the coin text indicator in the garage pages.
/// </summary>
public class GarageController : MonoBehaviour
{
    public GameObject tab;
    public GameObject carPage;
    public GameObject backgroundPage;
    public Text coinsCount;

    private const int UnselectedTabIndex = 0;
    private const int SelectedTabIndex = 1;
    private const int CarGaragePageTabIndex = 0;
    private const int BackGroundGaragePageTabIndex = 1;
    private GameObject[] _tabs;
    private List<GameObject> _garagePages;


    private void Start()
    {
        _garagePages = new List<GameObject>()
            {carPage, backgroundPage};

        var tabsCount = tab.transform.childCount;
        _tabs = new GameObject[tabsCount];
        for (var tabIndex = 0; tabIndex < tabsCount; tabIndex++)
        {
            _tabs[tabIndex] = tab.transform.GetChild(tabIndex).gameObject;
        }
    }

    private void OnEnable()
    {
        // Update Coin text when enter the garage.
        SetCoins();
    }

    public void OnEnterCarGaragePageButtonCLicked()
    {
        SetPage(carPage);
        SetTab(CarGaragePageTabIndex);
    }

    public void OnEnterBackgroundPageButtonClicked()
    {
        SetPage(backgroundPage);
        SetTab(BackGroundGaragePageTabIndex);
    }

    private void SetPage(GameObject targetPage)
    {
        foreach (var page in _garagePages)
        {
            page.SetActive(page.Equals(targetPage));
        }
    }

    private void SetTab(int targetTabIndex)
    {
        for (var tabIndex = 0; tabIndex < _tabs.Length; tabIndex++)
        {
            var isTabSelected = tabIndex == targetTabIndex;
            _tabs[tabIndex].transform.GetChild(UnselectedTabIndex).gameObject.SetActive(!isTabSelected);
            _tabs[tabIndex].transform.GetChild(SelectedTabIndex).gameObject.SetActive(isTabSelected);
        }
    }

    // Update coin text in the garage page.
    private void SetCoins()
    {
        coinsCount.text = GameDataController.GetGameData().CoinsOwned.ToString();
    }
}