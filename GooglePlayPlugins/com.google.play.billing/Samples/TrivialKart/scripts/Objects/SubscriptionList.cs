using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constant data for subscriptions in the game.
/// </summary>
public static class SubscriptionList
{
    public class Subscription
    {
        public class VipSubscribeButton
        {
            public VipSubscribeButton(string subscribeButtonTextWhenNoSubscription,
                string subscribeButtonTextWhenSubscribeToOthers,
                string subscribeButtonTextWhenSubscribeToThisSubscription)
            {
                SubscribeButtonTextWhenNoSubscription = subscribeButtonTextWhenNoSubscription;
                SubscribeButtonTextWhenSubscribeToOthers = subscribeButtonTextWhenSubscribeToOthers;
                SubscribeButtonTextWhenSubscribeToThisSubscription = subscribeButtonTextWhenSubscribeToThisSubscription;
            }

            public string SubscribeButtonTextWhenSubscribeToThisSubscription { get; }

            public string SubscribeButtonTextWhenSubscribeToOthers { get; }

            public string SubscribeButtonTextWhenNoSubscription { get; }

            public GameObject ButtonGameObj { get; set; }
        }

        public Subscription(string name, SubscriptionType type, float price, string productId,
            string subscribeButtonTextWhenNoSubscription,
            string subscribeButtonTextWhenSubscribeToOthers,
            string subscribeButtonTextWhenSubscribeToThisSubscription)
        {
            Name = name;
            Type = type;
            Price = price;
            ProductId = productId;
            SubscribeButton = new VipSubscribeButton(subscribeButtonTextWhenNoSubscription,
                subscribeButtonTextWhenSubscribeToOthers,
                subscribeButtonTextWhenSubscribeToThisSubscription);
        }

        public string Name { get; }

        public SubscriptionType Type { get; }

        public float Price { get; }

        public GameObject SubscribeButtonGameObj
        {
            get => SubscribeButton.ButtonGameObj;
            set => SubscribeButton.ButtonGameObj = value;
        }

        public string ProductId { get; }

        public VipSubscribeButton SubscribeButton { get; }
    }

    public static readonly Subscription SilverSubscription =
        new Subscription("silver VIP", SubscriptionType.SilverSubscription, 1.99f,
            "silver_subscription", " subscribe now! ", "downgrade", "subscribed");

    public static readonly Subscription GoldenSubscription =
        new Subscription("golden VIP", SubscriptionType.GoldenSubscription, 4.99f,
            "golden_subscription", " subscribe now! ", "upgrade!", "subscribed");

    public static readonly Subscription NoSubscription =
        new Subscription("No VIP", SubscriptionType.NoSubscription, 0f, "no_subscription", "", "", "");

    public static readonly List<Subscription> List = new List<Subscription>()
        {NoSubscription, SilverSubscription, GoldenSubscription};
}