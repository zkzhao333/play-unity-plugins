using System.Collections.Generic;
using UnityEngine;

// Constant data for subscriptions in game.
public static class SubscriptionList
{
    public class Subscription
    {
        public Subscription(string name, SubscriptionType type, float price, GameObject subscribeButton, string productId)
        {
            Name = name;
            Type = type;
            Price = price;
            SubscribeButton = subscribeButton;
            ProductId = productId;
        }

        public string Name { get; }

        public SubscriptionType Type { get; }

        public float Price { get; }

        public GameObject SubscribeButton { get; }
        
        public string ProductId { get; }
    }

    public static readonly Subscription SilverSubscription =
        new Subscription("silver VIP", SubscriptionType.SilverSubscription, 1.99f,
            GameObject.Find("silverSubscriptionButton").gameObject, "silver_subscription");

    public static readonly Subscription GoldenSubscription =
        new Subscription("golden VIP", SubscriptionType.GoldenSubscription, 4.99f,
            GameObject.Find("goldenSubscriptionButton").gameObject, "golden_subscription");

    public static readonly Subscription NoSubscription =
        new Subscription("No VIP", SubscriptionType.NoSubscription, 0f, new GameObject(), "no_subscription");

    public static readonly List<Subscription> List = new List<Subscription>()
        {NoSubscription, SilverSubscription, GoldenSubscription};

    // Get the subscription object by given subscription type.
    public static Subscription GetSubscriptionObjByType(SubscriptionType subscriptionType)
    {
        switch (subscriptionType)
        {
            case SubscriptionType.GoldenSubscription:
                return GoldenSubscription;
            case SubscriptionType.SilverSubscription:
                return SilverSubscription;
            case SubscriptionType.NoSubscription:
                return NoSubscription;
            default:
                Debug.Log("subscription type doesn't exist");
                return NoSubscription;
        }
    }
}
