using System;

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

// TODO: Update carInUseName to enum after merge
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
    public string carInUseName;
    public Ownership[] carIndexToOwnership;
    public int coinsOwned;
    public SubscriptionType subscriptionType;
    public BackgroundName backgroundNameInUse;

    private const int InitialCoinAmount = 20;
    private const int TotalCarCount = 4;


    public GameData()
    {
        coinsOwned = InitialCoinAmount;
        carIndexToOwnership = new Ownership[TotalCarCount];
        foreach (var car in CarList.List)
        {
            carIndexToOwnership[CarList.GetIndexByName(car.CarName)] = Ownership.NotOwned;
        }

        carIndexToOwnership[CarList.GetIndexByName("carSedan")] = Ownership.Owned;
        carInUseName = "carSedan";
        subscriptionType = SubscriptionType.NoSubscription;
        backgroundNameInUse = BackgroundName.BlueGrass;
    }

    public Car CarInUseObj => CarList.GetCarByName(carInUseName);

    public SubscriptionList.Subscription CurSubscriptionObj =>
        SubscriptionList.GetSubscriptionObjByType(subscriptionType);

    public int CoinsOwned => coinsOwned;

    // Return possible discount on in store items () 
    public float Discount => subscriptionType == SubscriptionType.GoldenSubscription ? 0.6f : 1;

    // Reduce coins owned 
    public void ReduceCoinsOwned(int reduceAmount)
    {
        coinsOwned -= reduceAmount;
    }

    // Increase coins owned
    public void IncreaseCoinsOwned(int increaseAmount)
    {
        coinsOwned += increaseAmount;
    }

    // Purchase a car
    public void PurchaseCar(Car car)
    {
        if (!car.IsPriceInDollar)
        {
            ReduceCoinsOwned((int) car.Price);
        };
        carIndexToOwnership[CarList.GetIndexByName(car.CarName)] = Ownership.Owned;
    }
    

    // Check if user owns a car with carName
    // Return true if the user owns it; Otherwise return false
    public bool CheckOwnership(string carName)
    {
        return carIndexToOwnership[CarList.GetIndexByName(carName)] == Ownership.Owned;
    }

    // Change car in use status
    public void ChangeCar(Car targetCar)
    {
        carInUseName = targetCar.CarName;
    }

    // Subscribe to a subscription
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