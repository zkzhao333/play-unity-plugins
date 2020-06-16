using System;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public GameObject tab;
    public GameObject gasPage;
    public GameObject coinPage;
    public GameObject carPage;
    public Text coinsCount;

    private GameObject[] _tabs;
    private int _tabsCount;
    
    // Start is called before the first frame update
    void Start()
    {
        _tabsCount = tab.transform.childCount;
        _tabs = new GameObject[_tabsCount];
        for (int i = 0; i < _tabsCount; i++)
        {
            _tabs[i] = tab.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        coinsCount.text = PlayerPrefs.GetInt("coins", 20).ToString();
    }

    public void EnterGasPage()
    {
        gasPage.SetActive(true);
        coinPage.SetActive(false);
        carPage.SetActive(false);
        SetTab(0);
    }

    public void EnterCoinPage()
    {
        coinPage.SetActive(true);
        gasPage.SetActive(false);
        carPage.SetActive(false);
        SetTab(1);
    }

    public void EnterCarPage()
    {
        carPage.SetActive(true);
        coinPage.SetActive(false);
        gasPage.SetActive(false);
        SetTab(2);
    }

    void SetTab(int targetTagIndex)
    {
        for (int i = 0; i < _tabsCount; i++)
        {
            if (i == targetTagIndex)
            {
                _tabs[i].transform.GetChild(0).gameObject.SetActive(false);
                _tabs[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                _tabs[i].transform.GetChild(0).gameObject.SetActive(true);
                _tabs[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    // update coins when enter store
    private void OnEnable()
    {
        SetCoins();
    }

    public void SetCoins()
    {
        coinsCount.text = PlayerPrefs.GetInt("coins", 20).ToString();
    }
}
