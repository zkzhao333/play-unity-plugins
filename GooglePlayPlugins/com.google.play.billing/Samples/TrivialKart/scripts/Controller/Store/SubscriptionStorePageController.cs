// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
    private static void SetSubscriptionStatusBasedOnOwnership()
    {
        var currentSubscription = GameDataController.GetGameData().CurSubscriptionObj;
        // If the player doesn't subscribe to any of subscription, set all subscription item to initial status.
        if (currentSubscription.Type == SubscriptionType.NoSubscription)
        {
            foreach (var subscription in SubscriptionList.List)
            {
                SetSubscribeButton(subscription, subscription.SubscribeButton.SubscribeButtonTextWhenNoSubscription,
                    true);
            }
        }
        else
        {
            foreach (var subscription in SubscriptionList.List)
            {
                if (subscription == currentSubscription)
                {
                    SetSubscribeButton(
                        subscription,
                        subscription.SubscribeButton.SubscribeButtonTextWhenSubscribeToThisSubscription,
                        false
                    );
                }
                else
                {
                    SetSubscribeButton(
                        subscription,
                        subscription.SubscribeButton.SubscribeButtonTextWhenSubscribeToOthers,
                        true
                    );
                }
            }
        }
    }

    private static void SetSubscribeButton(SubscriptionList.Subscription targetSubscription, string targetButtonText,
        bool isButtonInteractive)
    {
        // Do nothing if the player has not subscribed to any plan.
        if (targetSubscription.Type == SubscriptionType.NoSubscription)
        {
            return;
        }

        var targetButton = targetSubscription.SubscribeButtonGameObj;
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
        confirmPanel.SetActive(false);
        PurchaseController.PurchaseASubscription(GameDataController.GetGameData().CurSubscriptionObj,
            _subscriptionToSubscribe);
    }

    public void OnCancelSubscribeButtonClicked()
    {
        confirmPanel.SetActive((false));
    }
}