using System;
using UnityEngine;
using Object = UnityEngine.Object;

public enum Ownership
{
    NotOwned,
    Owned
}

public enum SubscriptionType
{
    NoSubscription,
    SilverSubscription,
    GoldenSubscription
}

public enum CarName
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
    public CarName carInUseName;
    public Ownership[] carIndexToOwnership;
    public Ownership[] backgroundNameToOwnership;
    public int coinsOwned;
    public SubscriptionType subscriptionType;
    public BackgroundName backgroundInUseName;

    private const int InitialCoinAmount = 20;
    private const int TotalCarCount = 4;
    private const int TotalBackgroundCount = 2;
    private PlayerController _playerController =  Object.FindObjectOfType<PlayerController>();
    private GameObject _backGroundImages = GameObject.Find("background/backGroundImages").gameObject;

    
    public GameData()
    {
        coinsOwned = InitialCoinAmount;
        carIndexToOwnership = new Ownership[TotalCarCount];
        foreach (var car in CarList.List)
        {
            carIndexToOwnership[(int) car.Name] = Ownership.NotOwned;
        }

        carIndexToOwnership[(int) CarList.CarSedan.Name] = Ownership.Owned;

        backgroundNameToOwnership = new Ownership[TotalBackgroundCount];
        foreach (var background in BackgroundList.List)
        {
            backgroundNameToOwnership[(int) background.Name] = Ownership.NotOwned;
        }

        backgroundNameToOwnership[(int) BackgroundList.BlueGrassBackground.Name] = Ownership.Owned;

        carInUseName = CarName.Sedan;
        subscriptionType = SubscriptionType.NoSubscription;
        backgroundInUseName = BackgroundName.BlueGrass;
    }

    public CarList.Car CarInUseObj => CarList.List[(int) carInUseName];

    public BackgroundList.Background BackgroundInUseObj => BackgroundList.List[(int) backgroundInUseName];

    public SubscriptionList.Subscription CurSubscriptionObj =>
        SubscriptionList.GetSubscriptionObjByType(subscriptionType);

    public int CoinsOwned => coinsOwned;

    // Return possible discount on in store items.
    public float Discount => subscriptionType == SubscriptionType.GoldenSubscription ? 0.6f : 1;

    // Reduce amount of coins owned.
    public void ReduceCoinsOwned(int reduceAmount)
    {
        coinsOwned -= reduceAmount;
        UpdateCoinText();
    }

    // Increase amount of coins owned.
    public void IncreaseCoinsOwned(int increaseAmount)
    {
        coinsOwned += increaseAmount;
        UpdateCoinText();
    }

    // TODO: Move interactions to GameData Controller.
    private void UpdateCoinText()
    {
        Object.FindObjectOfType<GameManager>().SetCoins();
        Object.FindObjectOfType<StoreController>().SetCoins();
    }

    // Purchase a car.
    public void PurchaseCar(CarList.Car car)
    {
        if (!car.IsPriceInDollar)
        {
            ReduceCoinsOwned((int) (car.Price * Discount));
        }

        carIndexToOwnership[(int) car.Name] = Ownership.Owned;
        // Make the refresh happen in car store page controller.
        Object.FindObjectOfType<CarStorePageController>()?.RefreshPage();
    }

    // Check if the user owns a specific car.
    // Return true if the user owns it; Otherwise return false.
    public bool CheckCarOwnership(CarList.Car car)
    {
        return carIndexToOwnership[(int) car.Name] == Ownership.Owned;
    }

    // Change car in use and apply the changes to the play page.
    public void ChangeCar(CarList.Car targetCar)
    {
        carInUseName = targetCar.Name;
        _playerController.UpdateCarInUse();
    }

    // Check if the user owns a specific background.
    // Return true if the user owns it; Otherwise return false.
    public bool CheckBackgroundOwnership(BackgroundList.Background background)
    {
        return backgroundNameToOwnership[(int) background.Name] == Ownership.Owned;
    }

    // Change background in use and apply the changes to the play page.
    public void ChangeBackground(BackgroundList.Background targetBackground)
    {
        backgroundInUseName = targetBackground.Name;
        
        foreach (Transform background in _backGroundImages.transform)
        {
            background.gameObject.GetComponent<SpriteRenderer>().sprite = targetBackground.ImageSprite;
        }
    }

    // Subscribe to a subscription.
    public void SubscriptTo(SubscriptionList.Subscription targetSubscription)
    {
        subscriptionType = targetSubscription.Type;
        backgroundNameToOwnership[(int) BackgroundName.Mushroom] = Ownership.Owned;
        ChangeBackground(BackgroundList.MushroomBackground);
    }

    // Unsubscribe from any exist subscription.
    public void Unsubscribe()
    {
        subscriptionType = SubscriptionType.NoSubscription;
        backgroundNameToOwnership[(int) BackgroundName.Mushroom] = Ownership.NotOwned;
        ChangeBackground(BackgroundList.BlueGrassBackground);
    }
}