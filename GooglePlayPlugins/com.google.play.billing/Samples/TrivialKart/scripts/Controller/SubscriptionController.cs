using UnityEngine;
using UnityEngine.UI;

// Controller for subscription page
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

    // Refresh the page
    private void RefreshPage()
    {
        confirmPanel.SetActive(false);
        CheckAndSetSubscriptionStatus();
    }

    // Check if the player already subscribed to a plan and change the subscribe button correspondingly
    private void CheckAndSetSubscriptionStatus()
    {
        foreach (var subscription in SubscriptionList.List)
        {
            SetSubscribeButton(subscription, " subscribe now! ", true);
        }

        SetSubscribeButton(_gameData.CurSubscriptionObj, "subscribed", false);
    }

    // TODO: Set buttons
    private void SetSubscribeButton(SubscriptionList.Subscription targetSubscription, string targetButtonText,
        bool isButtonInteractive)
    {
        // Do nothing if the player has not subscribed to any plan.
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

        // TODO: Play-billing-API
        bool confirmedPurchase = true;

        if (confirmedPurchase)
        {
            _gameData.SubscriptTo(_subscriptionToSubscribe);
            // TODO: Combine the below method to above method
            _gameManager.SaveGameData();
        }
        
        _backgroundController.SwitchToMushroomBackGround();
        RefreshPage();
    }

    public void OnCancelSubscribeButtonClicked()
    {
        confirmPanel.SetActive((false));
    }
}