using System;
using System.Linq;
using UnityEngine;

public enum Ownership
{
    Owned,
    NotOwned
}

public enum SubscriptionType
{
    NoSubscription,
    SilverSubscription,
    GoldenSubscription
}

// TODO: Update carInUseName to enum after merge.
public enum CarType
{
    Sedan,
    Truck,
    Jeep,
    Kart
}

public enum BackgroundName
{
    BlueGrass,
    Mushroom
}

// GameData stores all the items/data the player obtained.
[Serializable]
public class GameData
{
    // TODO: switch car in use from string to enum
    public string carInUseName;
    public Ownership[] carIndexToOwnership;
    public Ownership[] backgroundNameToOwnership;
    public int coinsOwned;
    public SubscriptionType subscriptionType;
    public BackgroundName backgroundNameInUse;

    private const int InitialCoinAmount = 20;
    private const int TotalCarCount = 4;
    private const int TotalBackgroundCount = 2;


    public GameData()
    {
        Debug.Log("initialize gamedata");
        coinsOwned = InitialCoinAmount;
        carIndexToOwnership = new Ownership[TotalCarCount];
        foreach (var car in CarList.List)
        {
            carIndexToOwnership[CarList.GetIndexByName(car.CarName)] = Ownership.NotOwned;
        }

        carIndexToOwnership[CarList.GetIndexByName("carSedan")] = Ownership.Owned;

        backgroundNameToOwnership = new Ownership[TotalBackgroundCount];
        foreach (var background in BackgroundList.List)
        {
            backgroundNameToOwnership[(int) background.Name] = Ownership.NotOwned;
        }

        backgroundNameToOwnership[(int) BackgroundList.BlueGrassBackground.Name] = Ownership.Owned;

        carInUseName = "carSedan";
        subscriptionType = SubscriptionType.NoSubscription;
        backgroundNameInUse = BackgroundName.BlueGrass;
        Debug.Log("finished initilizing game data");
    }

    public Car CarInUseObj => CarList.GetCarByName(carInUseName);

    public BackgroundList.Background BackgroundInUseObj => BackgroundList.List[(int) backgroundNameInUse];
    public SubscriptionList.Subscription CurSubscriptionObj =>
        SubscriptionList.GetSubscriptionObjByType(subscriptionType);

    public int CoinsOwned => coinsOwned;

    // Return possible discount on in store items.
    public float Discount => subscriptionType == SubscriptionType.GoldenSubscription ? 0.6f : 1;

    // Reduce amount of coins owned.
    public void ReduceCoinsOwned(int reduceAmount)
    {
        coinsOwned -= reduceAmount;
    }

    // Increase amount of coins owned.
    public void IncreaseCoinsOwned(int increaseAmount)
    {
        coinsOwned += increaseAmount;
    }

    // Purchase a car.
    public void PurchaseCar(Car car)
    {
        if (!car.IsPriceInDollar)
        {
            ReduceCoinsOwned((int) car.Price);
        }

        ;
        carIndexToOwnership[CarList.GetIndexByName(car.CarName)] = Ownership.Owned;
    }


    // Check if the user owns a specific car.
    // Return true if the user owns it; Otherwise return false.
    public bool CheckCarOwnership(Car car)
    {
        return carIndexToOwnership[CarList.GetIndexByName(car.CarName)] == Ownership.Owned;
    }

    // Change car in use.
    public void ChangeCar(Car targetCar)
    {
        carInUseName = targetCar.CarName;
    }

    // Check if the user owns a specific background.
    // Return true if the user owns it; Otherwise return false;
    public bool CheckBackgroundOwnership(BackgroundList.Background background)
    {
        return backgroundNameToOwnership[(int) background.Name] == Ownership.Owned;
    }
    // Change background in use.
    public void ChangeBackground(BackgroundList.Background targetBackground)
    {
        backgroundNameInUse = targetBackground.Name;
        GameObject backGroundImages = GameObject.Find("backGroundImages").gameObject;
        foreach (Transform background in backGroundImages.transform)
        {
            background.gameObject.GetComponent<SpriteRenderer>().sprite = targetBackground.ImageSprite;
        }
    }

    // Subscribe to a subscription.
    public void SubscriptTo(SubscriptionList.Subscription targetSubscription)
    {
        subscriptionType = targetSubscription.Type;
    }

    // Unsubscribe from any exist subscription.
    public void Unsubscribe()
    {
        subscriptionType = SubscriptionType.NoSubscription;
    }
}