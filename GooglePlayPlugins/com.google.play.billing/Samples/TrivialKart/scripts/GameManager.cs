
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject playPageCanvas;
    public GameObject storePageCanvas;
    public GameObject garagePageCanvas;

    public Text coinsCount;
    // Start is called before the first frame update
    public void Start()
    {
        // reset the coins when start the game
        PlayerPrefs.SetInt("coins", 20);
        SetCoins();
    }
    
    // set the coins count at the play page
    public void SetCoins()
    {
        coinsCount.text = PlayerPrefs.GetInt("coins", 20).ToString();
    }

    public void EnterStore()
    {
        storePageCanvas.SetActive(true);
        playPageCanvas.SetActive(false);
        garagePageCanvas.SetActive(false);
    }

    public void EnterPlayPage()
    {
        storePageCanvas.SetActive(false);
        playPageCanvas.SetActive(true);
        garagePageCanvas.SetActive(false);
    }

    public void EnterGaragePage()
    {
        storePageCanvas.SetActive(false);
        playPageCanvas.SetActive(false);
        garagePageCanvas.SetActive(true);
    }

    // check if the player is in play mode (page)
    public bool IsInPlayPage()
    {
        return playPageCanvas.activeInHierarchy;
    }
}
