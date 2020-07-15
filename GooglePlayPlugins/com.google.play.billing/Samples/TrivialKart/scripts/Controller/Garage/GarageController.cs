using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controller for the tab/page switch in the garage.
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
        // Set all store pages to inactive.
        foreach (var page in _garagePages)
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
    
    // Update coin text in the garage page.
    private void SetCoins()
    {
        coinsCount.text = GameDataController.GetGameData().CoinsOwned.ToString();
    }
}
