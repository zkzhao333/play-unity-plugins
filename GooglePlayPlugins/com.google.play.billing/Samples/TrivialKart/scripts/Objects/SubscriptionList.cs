using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constant data for subscriptions in the game.
/// </summary>
public static class SubscriptionList
{
    public class Subscription
    {
        public Subscription(string name, SubscriptionType type, float price, string productId)
        {
            Name = name;
            Type = type;
            Price = price;
            ProductId = productId;
        }

        public string Name { get; }

        public SubscriptionType Type { get; }

        public float Price { get; }

        public GameObject SubscribeButton { get; set; }
        
        public string ProductId { get; }
    }

    public static readonly Subscription SilverSubscription =
        new Subscription("silver VIP", SubscriptionType.SilverSubscription, 1.99f,
             "silver_subscription");

    public static readonly Subscription GoldenSubscription =
        new Subscription("golden VIP", SubscriptionType.GoldenSubscription, 4.99f,
            "golden_subscription");

    public static readonly Subscription NoSubscription =
        new Subscription("No VIP", SubscriptionType.NoSubscription, 0f, "no_subscription");

    public static readonly List<Subscription> List = new List<Subscription>()
        {NoSubscription, SilverSubscription, GoldenSubscription};
    
}
