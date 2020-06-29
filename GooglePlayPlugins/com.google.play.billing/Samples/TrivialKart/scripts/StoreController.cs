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
    private GameData _gameData;
    private const int UnselectedTabIndex = 0;
    private const int SelectedTabIndex = 1;
    private const int GasStorePageTabIndex = 0;
    private const int CoinStorePageTabIndex = 1;
    private const int CarStorePageTabIndex = 2;
    


    private void Awake()
    {
        _gameData = FindObjectOfType<GameManager>().GetGameData();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _tabsCount = tab.transform.childCount;
        _tabs = new GameObject[_tabsCount];
        for (int i = 0; i < _tabsCount; i++)
        {
            _tabs[i] = tab.transform.GetChild(i).gameObject;
        }
    }

    // keep the coin text updated
    private void Update()
    {
        SetCoins();
    }

    public void EnterGasPage()
    {
        gasPage.SetActive(true);
        coinPage.SetActive(false);
        carPage.SetActive(false);
        SetTab(GasStorePageTabIndex);
    }

    public void EnterCoinPage()
    {
        coinPage.SetActive(true);
        gasPage.SetActive(false);
        carPage.SetActive(false);
        SetTab(CoinStorePageTabIndex);
    }

    public void EnterCarPage()
    {
        carPage.SetActive(true);
        coinPage.SetActive(false);
        gasPage.SetActive(false);
        SetTab(CarStorePageTabIndex);
    }

    private void SetTab(int targetTagIndex)
    {
        for (int i = 0; i < _tabsCount; i++)
        {
            if (i == targetTagIndex)
            {
                _tabs[i].transform.GetChild(UnselectedTabIndex).gameObject.SetActive(false);
                _tabs[i].transform.GetChild(SelectedTabIndex).gameObject.SetActive(true);
            }
            else
            {
                _tabs[i].transform.GetChild(UnselectedTabIndex).gameObject.SetActive(true);
                _tabs[i].transform.GetChild(SelectedTabIndex).gameObject.SetActive(false);
            }
        }
    }
    

    public void SetCoins()
    {
        coinsCount.text = _gameData.coinOwned.ToString();
    }
}