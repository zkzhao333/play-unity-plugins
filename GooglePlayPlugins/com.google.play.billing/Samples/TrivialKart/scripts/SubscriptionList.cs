using System.Collections.Generic;
using UnityEngine;

public static class SubscriptionList
{
    public class Subscription
    {
        public Subscription(string name, SubscriptionType type, float price, GameObject subscribeButton)
        {
            Name = name;
            Type = type;
            Price = price;
            SubscribeButton = subscribeButton;
        }

        public string Name { get; }

        public SubscriptionType Type { get; }

        public float Price { get; }

        public GameObject SubscribeButton { get; }
    }

    public static readonly Subscription SilverSubscription =
        new Subscription("silver VIP", SubscriptionType.SilverSubscription, 1.99f,
            GameObject.Find("silverSubscriptionButton").gameObject);

    public static readonly Subscription GoldenSubscription =
        new Subscription("golden VIP", SubscriptionType.GoldenSubscription, 4.99f,
            GameObject.Find("goldenSubscriptionButton").gameObject);

    public static readonly Subscription NoSubscription =
        new Subscription("No VIP", SubscriptionType.NoSubscription, 0f, new GameObject());

    public static readonly List<Subscription> List = new List<Subscription>()
        {NoSubscription, SilverSubscription, GoldenSubscription};

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