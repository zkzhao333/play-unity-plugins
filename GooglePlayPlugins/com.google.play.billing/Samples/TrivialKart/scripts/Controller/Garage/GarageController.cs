using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controller for the tab/page switch in the garage
public class GarageController : MonoBehaviour
{
    public GameObject tab;
    public GameObject carPage;
    public GameObject backgroundPage;
    public Text coinsCount;
    
    private const int UnselectedTabIndex = 0;
    private const int SelectedTabIndex = 1;
    private const int carGaragePageTabIndex = 0;
    private const int backGroundGragePagetabIndex = 1;
    private GameObject[] _tabs;
    private int _tabsCount;
    private List<GameObject> _garagePages;
    
    private void Start()
    {
        _garagePages = new List<GameObject>()
            {carPage, backgroundPage};
        _tabsCount = tab.transform.childCount;
        _tabs = new GameObject[_tabsCount];
        for (var tabIndex = 0; tabIndex < _tabsCount; tabIndex++)
        {
            _tabs[tabIndex] = tab.transform.GetChild(tabIndex).gameObject;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Keep the coin text updated.
        SetCoins();
    }

    public void OnEnterCarGaragePageButtonCLicked()
    {
        SetPage(carPage);
        SetTab(carGaragePageTabIndex);
    }

    public void OnEnterBackgroundPageButtonClicked()
    {
        SetPage(backgroundPage);
        SetTab(backGroundGragePagetabIndex);
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
    private void SetCoins()
    {
        coinsCount.text = GameDataController.GetGameData().CoinsOwned.ToString();
    }
}
