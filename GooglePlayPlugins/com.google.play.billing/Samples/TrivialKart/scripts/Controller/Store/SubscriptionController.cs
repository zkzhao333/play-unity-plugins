using UnityEngine;
using UnityEngine.UI;

// Controller for subscription page.
public class SubscriptionController : MonoBehaviour
{
    public GameObject confirmPanel;
    public Text confirmText;

    private SubscriptionList.Subscription _subscriptionToSubscribe;
    
    
    private void OnEnable()
    {
        RefreshPage();
    }

    // Refresh the page.
    private void RefreshPage()
    {
        confirmPanel.SetActive(false);
        CheckAndSetSubscriptionStatus();
    }

    // Check if the player already subscribed to a plan and change the subscribe button correspondingly.
    private void CheckAndSetSubscriptionStatus()
    {
        foreach (var subscription in SubscriptionList.List)
        {
            SetSubscribeButton(subscription, " subscribe now! ", true);
        }

        SetSubscribeButton(GameDataController.GetGameData().CurSubscriptionObj, "subscribed", false);
    }

    // TODO: Set buttons.
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

        // TODO: Play-billing-API.
        bool confirmedPurchase = true;

        if (confirmedPurchase)
        {
            GameDataController.GetGameData().SubscriptTo(_subscriptionToSubscribe);
        }

        RefreshPage();
    }

    public void OnCancelSubscribeButtonClicked()
    {
        confirmPanel.SetActive((false));
    }
}