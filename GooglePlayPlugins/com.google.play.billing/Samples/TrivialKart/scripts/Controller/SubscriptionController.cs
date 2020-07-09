using UnityEngine;
using UnityEngine.UI;


public class SubscriptionController : MonoBehaviour
{
    public GameObject backGroundControllerGameObj;
    public GameObject confirmPanel;
    public Text confirmText;

    private BackgroundController _backgroundController;
    private SubscriptionList.Subscription _subscriptionToSubscribe;
    private GameData _gameData;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameData = _gameManager.GetGameData();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _backgroundController = backGroundControllerGameObj.GetComponent<BackgroundController>();
    }

    private void OnEnable()
    {
        RefreshPage();
    }

    // refresh the page
    private void RefreshPage()
    {
        confirmPanel.SetActive(false);
        CheckAndSetSubscriptionStatus();
    }

    // check if the player already subscribed to a plan and change the subscribe button correspondingly
    private void CheckAndSetSubscriptionStatus()
    {
        foreach (var subscription in SubscriptionList.List)
        {
            SetSubscribeButton(subscription, " subscribe now! ", true);
        }

        SetSubscribeButton(_gameData.CurSubscriptionObj, "subscribed", false);
    }

    // TODO: set buttons
    private void SetSubscribeButton(SubscriptionList.Subscription targetSubscription, string targetButtonText,
        bool isButtonInteractive)
    {
        // do nothing if the player has not subscribed to any plan.
        if (targetSubscription.Type == SubscriptionType.NoSubscription)
        {
            return;
        }

        var targetButton = targetSubscription.SubscribeButton;
        targetButton.transform.Find("Text").gameObject.GetComponent<Text>().text = targetButtonText;
        targetButton.GetComponent<Button>().interactable = isButtonInteractive;
    }

    public void OnSilverSubscribeButtonClicked()
    {
        _subscriptionToSubscribe = SubscriptionList.SilverSubscription;
        Subscribe();
    }

    public void OnGoldenSubscribeButtonClicked()
    {
        _subscriptionToSubscribe = SubscriptionList.GoldenSubscription;
        Subscribe();
    }

    private void Subscribe()
    {
        confirmPanel.SetActive(true);
        confirmText.text = "Would you like to subscribe to " + _subscriptionToSubscribe.Name + " with " +
                           _subscriptionToSubscribe.Price + "/month ?";
        
    }

    public void OnConfirmSubscribeButtonClicked()
    {
        confirmPanel.SetActive((false));

        // TODO: play-billing-API
        bool confirmedPurchase = true;

        if (confirmedPurchase)
        {
            _gameData.SubscriptTo(_subscriptionToSubscribe);
            // TODO: combine the below method to above method
            _gameManager.SaveGameData();
        }
        
        _backgroundController.SwitchToDesertBackGround();
        RefreshPage();
    }

    public void OnCancelSubscribeButtonClicked()
    {
        confirmPanel.SetActive((false));
    }
}