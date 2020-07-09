using System;

// TODO: move the enums in a seperate file
// prefer enum than bool
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

// TODO: update carInUseName to enum after merge
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

    // return possible discount on in store items () 
    public float Discount => subscriptionType == SubscriptionType.GoldenSubscription ? 0.6f : 1;

    // reduce coins owned 
    public void ReduceCoinsOwned(int reduceAmount)
    {
        coinsOwned -= reduceAmount;
    }

    // increase coins owned
    public void IncreaseCoinsOwned(int increaseAmount)
    {
        coinsOwned += increaseAmount;
    }

    // own a car
    public void PurchaseCar(Car car)
    {
        if (!car.IsPriceInDollar)
        {
            ReduceCoinsOwned((int) car.Price);
        };
        carIndexToOwnership[CarList.GetIndexByName(car.CarName)] = Ownership.Owned;
    }
    

    // check if user owns a car with carName
    // return true if the user owns it; otherwise return false
    public bool CheckOwnership(string carName)
    {
        return carIndexToOwnership[CarList.GetIndexByName(carName)] == Ownership.Owned;
    }

    // change car in use
    public void ChangeCar(Car targetCar)
    {
        carInUseName = targetCar.CarName;
    }

    public void SubscriptTo(SubscriptionList.Subscription targetSubscription)
    {
        subscriptionType = targetSubscription.Type;
    }

    public void Unsubscribe()
    {
        subscriptionType = SubscriptionType.NoSubscription;
    }
}