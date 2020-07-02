using System;

[Serializable]
public class GameData
{
    public string carNameInUse;
    public bool[] carIndexToOwnership;
    public int coinOwned;

    private Car _carObjInUse;
    private string _dataPath;
    private const int InitialCoinAmount = 20;
    private const int TotalCarCount = 4;
    private const bool Owned = true;
    private const bool NotOwned = false;

    public GameData(string dataPath)
    {
        _dataPath = dataPath;
        coinOwned = InitialCoinAmount;
        carIndexToOwnership = new bool[TotalCarCount];
        carIndexToOwnership[CarList.GetIndexByName("carSedan")] = Owned;
        carIndexToOwnership[CarList.GetIndexByName("carTruck")] = NotOwned;
        carIndexToOwnership[CarList.GetIndexByName("carJeep")] = NotOwned;
        carIndexToOwnership[CarList.GetIndexByName("carKart")] = NotOwned;
        carNameInUse = "carSedan";
    }

    // update car object in use
    public void SetCarObjInUse()
    {
        _carObjInUse = CarList.GetCarByName(carNameInUse);
    }

    // get the car object in use
    public Car GetCarObjInUse()
    {
        return _carObjInUse;
    }


    // reduce coins owned 
    public void ReduceCoinsOwned(int reduceAmount)
    {
        coinOwned -= reduceAmount;
    }

    // increase coins owned
    public void IncreaseCoinsOwned(int increaseAmount)
    {
        coinOwned += increaseAmount;
    }

    // own a car
    public void PurchaseCar(string carName)
    {
        carIndexToOwnership[CarList.GetIndexByName(carName)] = Owned;
    }

    // check if user owns a car with carName
    // return true if the user owns it; otherwise return false
    public bool CheckOwnership(string carName)
    {
        return carIndexToOwnership[CarList.GetIndexByName(carName)];
    }
}