using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for subscription store page.
/// It listens to subscription subscribe button click events,
/// initializing the purchase flow when subscribe button clicked.
/// </summary>
public class SubscriptionStorePageController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private SubscriptionList.Subscription _subscriptionToSubscribe;
    
    
    private void OnEnable()
    {
        RefreshPage();
    }

    // Refresh the page.
    public void RefreshPage()
    {
        confirmPanel.SetActive(false);
        SetSubscriptionStatusBasedOnOwnership();
    }

    // Check if the player already subscribed to a plan and change the subscribe button correspondingly.
    private void SetSubscriptionStatusBasedOnOwnership()
    {
        // TODO: Set upgrade and downgrade button text.
        foreach (var subscription in SubscriptionList.List)
        {
            SetSubscribeButton(subscription, " subscribe now! ", true);
        }

        SetSubscribeButton(GameDataController.GetGameData().CurSubscriptionObj, "subscribed", false);
    }
    
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
        PurchaseController.BuyProductId(_subscriptionToSubscribe.ProductId);
        RefreshPage();
    }

    public void OnCancelSubscribeButtonClicked()
    {
        confirmPanel.SetActive((false));
    }
}